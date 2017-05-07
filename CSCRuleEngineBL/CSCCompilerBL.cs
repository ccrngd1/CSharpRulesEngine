using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq; 
using RulesEngine.DAL;
using LawsonCS.RulesEngine.Data;

namespace LawsonCS.Business.RulesEngine.Compiler
{
    public class CSCCompilerBL : ICompilerBL
    {
        private readonly string[] _codeFooter = 
        {
            "}",
            "}"
        };

        private readonly IRulesEngineDAL _rulesDAL;
        private readonly string _ruleTable;

        public CSCCompilerBL(IRulesEngineDAL iDAL, string ruleTable)
        {
            _rulesDAL = iDAL;
            _ruleTable = ruleTable;

            RefreshRules();
        } 

        public DataTable RefreshRules()
        {
            return GetEnabledRules(new RuleAppliedFilter {RuleTableName = _ruleTable}); 
        }

        private DataTable GetEnabledRules(RuleAppliedFilter filter)
        {
            return _rulesDAL.GetEnabledRules(filter);
        }

        private class RuleCacheRefresh
        {
            public int RuleAppliedId;
            public int CodeId;
            public ulong CodeHash;
            public int StartLineNumber;
            public int EndLineNumber;
        }

        //private List<RuleCacheRefresh> GetRuleInfoFromFile(string filename, ref List<RuleAppliedBase> rules)
        private List<RuleCacheRefresh> GetRuleInfoFromFile(string filename)
        {
            ////sample
            //public static
            //ZirMed.Claims.Objects.Transactions.IClaim Z_1_4_6(ZirMed.Claims.Objects.Transactions.IClaim input)
            //{
            //    throw new System.Exception();
            //}

            var refreshResults = new List<RuleCacheRefresh>();

            var currentLine = 0;

            using (TextReader tr = new StreamReader(new FileStream(filename, FileMode.OpenOrCreate)))
            {
                var line = tr.ReadLine();

                var insideClass = false;
                var insideMethod = false;

                var bracketCount = 0;
                var methodStartLine = 0;
                
                while(line!=null)
                {
                    currentLine++;
                    bracketCount += line.Split('{').Length - 1;
                    bracketCount -= line.Split('}').Length- 1;

                    if (bracketCount < 2) insideClass = false;
                    if (bracketCount < 3)
                    {
                        if (insideMethod && methodStartLine>0)
                        {
                            RuleCacheRefresh foundMethodCache = refreshResults.FirstOrDefault(c => c.StartLineNumber == methodStartLine);

                            if(foundMethodCache==null)
                                throw new NullReferenceException("srsly, why is this null, we just found the method name and marked it a few lines ago");

                            foundMethodCache.EndLineNumber = currentLine;
                        }

                        methodStartLine = 0;
                        insideMethod = false;
                    }

                    if (line.Contains("public static class"))
                    {
                        insideClass = true;
                        line = tr.ReadLine();
                        continue;
                    }

                    if (!insideClass)
                    {
                        line = tr.ReadLine();
                        continue;
                    }

                    if (line.Contains("public static") && !insideMethod)
                    {
                        insideMethod = true;

                        string[] splitMethodName = line.Split(new[] { ' ' }, StringSplitOptions.None);

                        if (splitMethodName.Any())
                        {
                            var methodName = splitMethodName[3];
                            string[] methodNameSplit = methodName.Split(new[] { '_' }, StringSplitOptions.None);
                            methodNameSplit[methodNameSplit.Length - 1] =
                                methodNameSplit[methodNameSplit.Length - 1].Substring(0, methodNameSplit[methodNameSplit.Length - 1].Length - 1);

                            if (methodNameSplit.Any())
                            {
                                var temp = new RuleCacheRefresh
                                {
                                    CodeHash = ulong.Parse(methodNameSplit[1]),
                                    CodeId = int.Parse(methodNameSplit[2])
                                };

                                if (methodNameSplit.Length > 3 && !string.IsNullOrWhiteSpace(methodNameSplit[3]))
                                    temp.RuleAppliedId = int.Parse(methodNameSplit[3]);

                                RuleCodeCache tempCode = _rulesDAL.DALCache.RuleCodes.FirstOrDefault(c => c.RuleCodeId == temp.CodeId);

                                if (tempCode != null)
                                {
                                    tempCode.ExistsInCache = true;
                                    if (tempCode.CodeHash != temp.CodeHash)
                                    {
                                        temp.StartLineNumber = methodStartLine = currentLine;
                                        refreshResults.Add(temp);
                                    }
                                    else
                                        tempCode.NeedsRefresh = false;
                                }
                            }
                        }
                    }

                    line = tr.ReadLine();
                }
            }

            return refreshResults;
        }

        private void WriteRuleCodeToStream(TextWriter tw, RuleCodeCache codeCache, int? ruleAppliedId)
        {
            var methodName = string.Format("Z_{0}_{1}_{2}",codeCache.CodeHash, codeCache.RuleCodeId, ruleAppliedId);

            ////public static outputtype methodName(inputtype input){
            //// ruleCode }
            //// 
            tw.Write("public static ");

            RuleCodeIOType outputType = _rulesDAL.DALCache.RuleCodeIOTypes.FirstOrDefault(c=>c.RuleCodeIOTypeId == codeCache.OutputTypeId);

            if (outputType == null)
                throw new KeyNotFoundException("Output type not found in the cache");

            tw.Write(outputType.TypeName);

            tw.Write(" ");
            tw.Write(methodName);
            tw.Write("( ");

            RuleCodeIOType inputType = _rulesDAL.DALCache.RuleCodeIOTypes.FirstOrDefault(c => c.RuleCodeIOTypeId == codeCache.InputTypeId);

            if (inputType == null)
                throw new KeyNotFoundException("Input Type not found in the cache");

            tw.Write(inputType.TypeName);

            tw.Write(" obj)");
            tw.WriteLine("{");
            
            if (codeCache.CodeText == null)
                throw new NullReferenceException("can't have null code in CompileRules");

            tw.WriteLine(codeCache.CodeText);

            tw.WriteLine("}");
        }

        private void PrepFile(Stream csFile, Stream tempFile, List<RuleCacheRefresh> linesToRemoveFromFile)
        {
            var totalLines = 0;

            using (var sr = new StreamReader(csFile))
            using (var sw = new StreamWriter(tempFile))
            {
                while (sr.ReadLine() != null)
                {
                    totalLines++;
                }

                csFile.Position = 0;
                sr.BaseStream.Position = 0;

                string line;
                var currentLine = 0;

                int foundStart = 0, foundEnd = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    //have to count lines because we want to cut off the footer
                    currentLine++;

                    //Don't read the last X lines as that is definitely the footer and we will add that back in after we add in the updated rule code
                    if (currentLine > totalLines - _codeFooter.Length) continue;

                    RuleCacheRefresh toWork = linesToRemoveFromFile.FirstOrDefault(c => c.StartLineNumber == currentLine);

                    if (toWork != null)
                    {
                        foundStart = currentLine;
                        foundEnd = toWork.EndLineNumber;
                    }

                    if (currentLine > foundEnd)
                    {
                        foundStart = 0;
                        foundEnd = 0;
                    }

                    if (foundStart == 0 || foundEnd == 0)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        public CompilerResults CompileRules(List<RuleAppliedBase> rules)
        {
            if (!rules.Any()) return null;

            List<int> rulesToPull = rules.Select(c => c.CodeId).ToList();
            rulesToPull.AddRange(rules.Where(c=>c.ConditionCodeId!=null).Select(c => (int)c.ConditionCodeId).ToList());

            _rulesDAL.DALCache.RefreshRuleCodes(rulesToPull, true);
            
            string catName = "", subCatName = "";

            RuleCategory a = _rulesDAL.DALCache.RuleCategories.FirstOrDefault(c =>
            {
                RuleAppliedBase ruleAppliedBase = rules.FirstOrDefault();
                return ruleAppliedBase != null && c.RuleCategoryId == ruleAppliedBase.RuleCategoryId;
            });
            if (a != null) catName = a.CategoryName;

            RuleCategory b = _rulesDAL.DALCache.RuleCategories.FirstOrDefault(c =>
            {
                RuleAppliedBase ruleAppliedBase = rules.FirstOrDefault();
                return ruleAppliedBase != null && c.RuleCategoryId == ruleAppliedBase.RuleSubCategoryId;
            });
            if (b != null) subCatName = b.CategoryName;

            var tempFileName = Guid.NewGuid().ToString();
            var realFileName = catName + "_" + subCatName + ".cs";

            var codeFileExists = false;
            var linesToRemove = new List<RuleCacheRefresh>(); 

            //if the file already exists, we need to read through and get info on the what code was written out
            //we also need to copy over all code that has the same method signature into a new temp file
            //afterwards, we will insert the new/altered code at the end of the temp file and swap them out.
            if (File.Exists(realFileName))
            {
                linesToRemove = GetRuleInfoFromFile(realFileName);

                using (FileStream realFs = File.OpenRead(realFileName))
                using (FileStream tempFs = File.Create(tempFileName))
                {
                    PrepFile(realFs, tempFs , linesToRemove);
                }

                codeFileExists = true;
            } 

            _rulesDAL.GetRuleCode(_rulesDAL.DALCache.RuleCodes.Where(c => c.NeedsRefresh || !c.ExistsInCache).ToList());

            using (TextWriter tw = new StreamWriter(tempFileName, codeFileExists))
            {

                if (codeFileExists)
                {
                    foreach (RuleCacheRefresh ruleRemoved in linesToRemove)
                    {
                        RuleCodeCache code =
                            _rulesDAL.DALCache.RuleCodes.FirstOrDefault(d => d.RuleCodeId == ruleRemoved.CodeId);

                        if (code == null)
                            throw new NullReferenceException("code not found");

                        int? tempRuleAppId = null;

                        if (ruleRemoved.RuleAppliedId > 0)
                            tempRuleAppId = ruleRemoved.RuleAppliedId;

                        WriteRuleCodeToStream(tw, code, tempRuleAppId);
                    }
                }
                else
                {
                    tw.WriteLine(
                        "using System.Linq; using System.Collections; using System.Collections.Generic; namespace LawsonCS.RulesEngine { ");
                    //example
                    //public static class Validator_PostEnrollment
                    tw.WriteLine(" public static class " + catName + "_" + subCatName + "{");

                    foreach (RuleAppliedBase rule in rules)
                    {
                        RuleCodeCache code =
                            _rulesDAL.DALCache.RuleCodes.FirstOrDefault(d => d.RuleCodeId == rule.CodeId);

                        if (code == null)
                            throw new NullReferenceException("code not found");

                        //we only want to write the rule out if it is marked as needsRefresh
                        RuleCodeCache foundRuleCacheCheck =
                            _rulesDAL.DALCache.RuleCodes.FirstOrDefault(c => c.RuleCodeId == rule.CodeId);

                        if (foundRuleCacheCheck != null && foundRuleCacheCheck.NeedsRefresh)
                            WriteRuleCodeToStream(tw, code, rule.RuleAppliedId);
                    }

                    foreach (RuleAppliedBase ruleCond in rules.Where(c => c.ConditionCodeId != null))
                    {
                        if (ruleCond.ConditionCodeId == null) continue;

                        RuleCodeCache code =
                            _rulesDAL.DALCache.RuleCodes.FirstOrDefault(
                                d => d.RuleCodeId == ruleCond.ConditionCodeId.Value);

                        if (code == null)
                            throw new NullReferenceException("code not found");

                        //we only want to write the rule out if it is marked as needsRefresh
                        RuleCodeCache foundRuleCacheCheck =
                            _rulesDAL.DALCache.RuleCodes.FirstOrDefault(c => c.RuleCodeId == ruleCond.CodeId);

                        if (foundRuleCacheCheck != null && foundRuleCacheCheck.NeedsRefresh)
                            WriteRuleCodeToStream(tw, code, null);
                    }
                }

                foreach (var s in _codeFooter)
                {
                    tw.WriteLine(s);
                }

                tw.Flush();
            }

            foreach (RuleCodeCache ruleCodeCache in _rulesDAL.DALCache.RuleCodes)
            {
                ruleCodeCache.CodeText = null;
                ruleCodeCache.NeedsRefresh = false;
            }

            File.Copy(tempFileName, realFileName, true);
            File.Copy(tempFileName, ConfigurationManager.AppSettings["NonStandardLogLocation"] +"\\" + DateTime.Now.ToString("yy-MM-dd@HH mm ss") +"_" + realFileName ,true);

            File.Delete(tempFileName); 
            File.Delete(_ruleTable + ".dll");

            var compilerparams = new CompilerParameters()
            {
                GenerateExecutable = false,
                GenerateInMemory = false,
                IncludeDebugInformation = false,
                TreatWarningsAsErrors = false,
                CompilerOptions = string.Format("/lib:{0}", AppDomain.CurrentDomain.BaseDirectory),
                OutputAssembly = _ruleTable+".dll", 
            };

            List<string> fileList = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory).Where(c => c.Substring(c.Length - 3) == "dll").ToList();

            compilerparams.ReferencedAssemblies.AddRange(fileList.ToArray());
            compilerparams.ReferencedAssemblies.AddRange(Directory.GetFiles(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5").Where(c=>c.EndsWith(".dll") && !c.EndsWith("mscorlib.dll")).ToArray());

            CompilerResults results = CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromFile(compilerparams, realFileName);
            
            if(results.Errors.HasErrors)
                throw new Exception(results.Errors[0].ErrorText);

            return results;

        }

        public string CurrentRuleTable()
        {
            return _ruleTable;
        }

        //todo: get this back in
        //private void DomainLoadCallBack()
        //{
        //    Assembly assy = Assembly.LoadFrom(Environment.CurrentDirectory);

        //    assy.GetType("CLASS_CONTAINING_STATIC_METHOD")
        //        .InvokeMember("STATIC_METHOD",
        //            BindingFlags.Public |
        //            BindingFlags.Static |
        //            BindingFlags.InvokeMethod,
        //            null,
        //            null,
        //            null);

        //    var expTypes = assy.GetExportedTypes();
        //}
    } 
}

