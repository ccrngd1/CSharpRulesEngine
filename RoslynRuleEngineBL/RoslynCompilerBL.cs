//using System;
//using System.Collections.Generic;
//using System.Data;
////using System.Diagnostics;
////using System.Linq;
////using System.Text;
////using Microsoft.CodeAnalysis.Scripting.CSharp;
////using Microsoft.CodeAnalysis.Scripting;
////using System.Runtime.InteropServices;
//using RoslynRuleEngineDL;
//using RulesEngine.DAL; 
//using ZirMed.RulesEngine.Data;

//namespace Zirmed.RulesEngine 
//{
//    public class RoslynRuleEngine : ICompilerBL
//    {
//        private readonly IRulesEngineDAL _rulesDAL;

//        private RoslynRuleEngine()
//        {
//            throw new NotImplementedException("Can't call the rules engine without specifying a rulesEngineDAL");
//        }

//        public RoslynRuleEngine(IRulesEngineDAL iDAL)
//        {
//            _rulesDAL = iDAL;
//        }

//        public IEnumerable<RuleCategory> GetRuleCategories()
//        {
//            return _rulesDAL.GetAvailableCategories();
//        } 

//        public DataTable GetEnabledRules(RuleAppliedFilter filter)
//        {
//            return _rulesDAL.GetEnabledRules(filter);
//        }

//        #region RoslynCode
//        //public static T RunRule<T>(T input, IEnumerable<string> codes, List<string> addlNamespaces)
//        //{
//        //    string Ttype = typeof(T).Name;

//        //    var options = ScriptOptions.Default.AddSearchPaths(Environment.CurrentDirectory)
//        //    .AddSearchPaths(RuntimeEnvironment.GetRuntimeDirectory())
//        //    .AddReferences(typeof(T).Assembly)
//        //    .AddNamespaces("System").AddNamespaces(addlNamespaces.ToArray());

//        //    string usingStatement = addlNamespaces.Aggregate("using System; ", (current, ns) => current + ("using " + ns + "; "));

//        //    foreach (var code in codes)
//        //    {
//        //        string s1 = usingStatement + Ttype + " RunClaim(" + Ttype + " input) {" + code + " return input;}";
//        //        string s2 = " new Func<" + Ttype + ", " + Ttype + " > (RunClaim)";

//        //        var funcScript = CSharpScript.RunAsync<Func<T, T>>(s1 + s2,
//        //            options)
//        //            .Result.ReturnValue;

//        //        input = (T) Convert.ChangeType(funcScript(input), typeof (T));
//        //    }

//        //    return input;
//        //}

//        //public TOut RunRule<TIn, TOut>(TIn input, string code, List<string> addlNamespaces )
//        //{
//        //    string TTypeIn = typeof(TIn).Name;
//        //    string TTypeOut = typeof(TOut).Name;

//        //    var options = ScriptOptions.Default.AddSearchPaths(Environment.CurrentDirectory)
//        //    .AddSearchPaths(RuntimeEnvironment.GetRuntimeDirectory())
//        //    .AddReferences(typeof(TIn).Assembly)
//        //    .AddReferences(typeof(TOut).Assembly)
//        //    .AddNamespaces("System").AddNamespaces(addlNamespaces.ToArray());

//        //    string usingStatement = addlNamespaces.Aggregate("using System; ", (current, ns) => current + ("using " + ns + "; "));

//        //    string s1 =  usingStatement + TTypeOut + " RunClaim(" + TTypeIn + " input) {" + code + "}";
//        //    string s2 = " new Func<" + TTypeIn + ", " + TTypeOut + " > (RunClaim)";

//        //    var funcScript = CSharpScript.RunAsync<Func<TIn, TOut>>(s1 + s2,
//        //        options) 
//        //        .Result.ReturnValue; 

//        //    return (TOut)Convert.ChangeType(funcScript(input), typeof(TOut));
//        //}

//        //public T RunCLPRules<T>(T input, string code)
//        //{
//        //    string Ttype = typeof(T).Name;

//        //    var options = ScriptOptions.Default.AddSearchPaths(Environment.CurrentDirectory)
//        //    .AddSearchPaths(RuntimeEnvironment.GetRuntimeDirectory())
//        //    .AddReferences(typeof(T).Assembly)
//        //    .AddNamespaces("ZirMed.Claims.Objects.Transactions.CLP")
//        //    .AddNamespaces("System");

//        //    string s1 = "using System; using ZirMed.Claims.Objects.Transactions.CLP;" + Ttype + " RunClaim(" + Ttype + " input) {" + code + " return input;}";
//        //    string s2 = " new Func<" + Ttype + ", " + Ttype + " > (RunClaim)";

//        //    var funcScript = CSharpScript.RunAsync<Func<T, T>>(s1 + s2,
//        //        options)
//        //        .Result.ReturnValue;

//        //    return (T)Convert.ChangeType(funcScript(input), typeof(T));
//        //}

//        //public bool RunCLPConditionRules<T>(T input, string code)
//        //{
//        //    string Ttype = typeof(T).Name;

//        //    var options = ScriptOptions.Default.AddSearchPaths(Environment.CurrentDirectory)
//        //    .AddSearchPaths(RuntimeEnvironment.GetRuntimeDirectory())
//        //    .AddReferences(typeof(T).Assembly)
//        //    .AddReferences(typeof(System.Linq.Enumerable).Assembly)
//        //    .AddNamespaces("ZirMed.Claims.Objects.Transactions.CLP")
//        //    .AddNamespaces("System")
//        //    .AddNamespaces("System.Linq");

//        //    string s1 = "using System; using System.Linq; using ZirMed.Claims.Objects.Transactions.CLP; bool RunClaim(" + Ttype + " input) {" + code + "}";
//        //    string s2 = " new Func<" + Ttype + ", " + typeof(bool).Name + " > (RunClaim)";
             
//        //    var funcScript = CSharpScript.RunAsync<Func<T, bool>>(s1 + s2,
//        //        options)
//        //        .Result.ReturnValue;

//        //    return (bool)Convert.ChangeType(funcScript(input), typeof(bool)); 
//        //}

//        //public T RunCLIRules<T>(T input, string code)
//        //{
//        //    string Ttype = typeof(T).Name;

//        //    var options = ScriptOptions.Default.AddSearchPaths(Environment.CurrentDirectory)
//        //    .AddSearchPaths(RuntimeEnvironment.GetRuntimeDirectory())
//        //    .AddReferences(typeof(T).Assembly)
//        //    .AddNamespaces("ZirMed.Claims.Objects.Transactions.CLI")
//        //    .AddNamespaces("System");

//        //    string s1 = "using System; using ZirMed.Claims.Objects.Transactions.CLI;" + Ttype + " RunClaim(" + Ttype + " input) {" + code + " return input;}";
//        //    string s2 = " new Func<" + Ttype + ", " + Ttype + " > (RunClaim)";

//        //    var funcScript = CSharpScript.RunAsync<Func<T, T>>(s1 + s2,
//        //        options) 
//        //        .Result.ReturnValue; 

//        //    return (T)Convert.ChangeType(funcScript(input), typeof(T)); 
//        //}
//        #endregion

//        public TOut RunRules<TOut, TIn>(List<RuleAppliedBase> rules, TIn input)
//        {
//            throw new NotImplementedException();
//        }

//        public DataTable RefreshRules()
//        {
//            throw new NotImplementedException();
//        }
//    } 
//}
