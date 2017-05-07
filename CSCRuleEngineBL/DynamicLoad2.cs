//using System;
//using System.CodeDom.Compiler;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Sockets;
//using System.Reflection; 
//using Microsoft.CSharp;

////http://www.codeproject.com/Tips/715891/Compiling-Csharp-Code-at-Runtime
//namespace CSCRuleEngineBL.CodeProject
//{
//    public class DynamicLoad2
//    {
//        public static MethodInfo CreateFunction(string function)
//        {
//            string code = @"
//                using System;
            
//                namespace UserFunctions
//                {                
//                    public class BinaryFunction
//                    {                
//                        public static double Function(double x, double y)
//                        {
//                            return func_xy;
//                        }
//                    }
//                }
//            ";

//            string finalCode = code.Replace("func_xy", function);

//            CSharpCodeProvider provider = new CSharpCodeProvider();  
//            CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), finalCode);

//            Type binaryFunction = results.CompiledAssembly.GetType("UserFunctions.BinaryFunction");
//            return binaryFunction.GetMethod("Function");
//        }

//        public void Test()
//        {
//            DateTime start;
//            DateTime stop;
//            double result;
//            int repetitions = 1000000; 

//            MethodInfo function = CreateFunction("x + y");

//            Func<double, double, double> betterFunction = (Func<double, double, double>)Delegate.CreateDelegate
//(typeof(Func<double, double, double>), function);

//            double sum1 = 0;
//            double sum2 = 0;
//            double sum3 = 0; 

//            Console.WriteLine("Per {0} iterations", repetitions);

//            start = DateTime.Now;
//            for (int i = 0; i < repetitions; i++)
//            {
//                result = OriginalFunction(2, 3);
//            }
//            stop = DateTime.Now;
//            sum1 += (stop - start).TotalMilliseconds;
//            Console.WriteLine("Original - time: {0} ms", (stop - start).TotalMilliseconds);

//            start = DateTime.Now;
//            for (int i = 0; i < repetitions; i++)
//            {
//                result = (double) function.Invoke(null, new object[] {2, 3});
//            }
//            stop = DateTime.Now;
//            sum2 += (stop - start).TotalMilliseconds;
//            Console.WriteLine("Reflection - time: {0} ms", (stop - start).TotalMilliseconds);

//            start = DateTime.Now;
//            for (int i = 0; i < repetitions; i++)
//            {
//                result = betterFunction(2, 3);
//            }
//            stop = DateTime.Now;
//            sum3 += (stop - start).TotalMilliseconds;
//            Console.WriteLine("Delegate - time: {0} ms", (stop - start).TotalMilliseconds); 


//            Console.Write("Per Call Avg");
//            Console.WriteLine((decimal)(sum1/ repetitions)); 

//            Console.WriteLine((decimal)(sum2 / repetitions)); 

//            Console.WriteLine((decimal)(sum3 / repetitions)); 
//        }

//        public List<TempRules> Test2(string finalCode)
//        {
//            var rules = new List<TempRules>();

//            CSharpCodeProvider provider = new CSharpCodeProvider();
//            var compilerparams = new CompilerParameters();
//            compilerparams.GenerateExecutable = false;
//            compilerparams.GenerateInMemory = false;
//            compilerparams.IncludeDebugInformation = false;
//            compilerparams.TreatWarningsAsErrors = false;
//            compilerparams.CompilerOptions = string.Format("/lib:{0}", AppDomain.CurrentDomain.BaseDirectory);

//            var fileList = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory).Where(c => c.Substring(c.Length - 3) == "dll").ToList();

//            compilerparams.ReferencedAssemblies.AddRange(fileList.ToArray());

//            CompilerResults results = provider.CompileAssemblyFromSource(compilerparams, finalCode);

//            try
//            {
//                Type binaryFunction = results.CompiledAssembly.GetType("Zirmed.RulesEngine.TestRules");
                
//                for (int i = 0; i < 6*1000; i++)
//                {
//                    var tr = new TempRules()
//                    {
//                        ruleCode = null,
//                        ruleId = i,
//                        ruleName = "TestRules" + i%6,
//                    };

//                    var r = binaryFunction.GetMethod(tr.ruleName);

//                    tr.ruleRun1 = (Func<IClaim, IClaim>)Delegate.CreateDelegate
//                        (typeof(Func<IClaim, IClaim>), r);

//                    //tr.ruleRun2 = Delegate.CreateDelegate
//                    //    (typeof(Func<IClaim, IClaim>), r);

//                    rules.Add(tr);
//                }
//            }
//            catch(Exception ex) { Console.WriteLine(ex.Message);}

//            return rules;
//        } 
        
//        public double OriginalFunction(double x, double y)
//        {
//            return x + y;
//        } 
//    }

//    public class TempRules
//    {
//        public string ruleName;
//        public int ruleId;
//        public string ruleCode;
//        //public delegate TOut ruleRun2<TOut, TIn>(TIn input);
//        public Func<IClaim, IClaim> ruleRun1;
//    }

//    public interface IClaim { }
//} 

