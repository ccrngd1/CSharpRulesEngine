using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using Antlr4;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using RoslynRuleEngineConsole.xpath1w3;
using RulesEngine.DAL;
using Zirmed.Claims.Workflow.RulesEngine;
using ZirMed.Architecture.Utilities.CommandLine;


namespace RoslynRuleEngineConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {

#if DEBUG
            //args = new[] { "/src:workflow" };
            args = new[] { "/src:test" };
#endif

            ProcessArguments pa = new ProcessArguments();
            if (!ZirMed.Architecture.Utilities.CommandLine.Parser.ParseArgumentsWithUsage(args, pa))
            {
                Console.WriteLine("bad parameters");
                Console.ReadLine();
            }
            switch (pa.src)
            {
                case "workflow":
                    workflowParse();
                    break;
                case "validation":
                    validationParse();
                    break;
                case "edit":
                    break;
                case "test":
                    Test();
                    break;
                default:
                    Console.WriteLine("No option specified");
                    break;
            }
            var s = Console.ReadLine();
        }

        private static void workflowParse()
        {
            AntlrInputStream w3inputstream;

            BaseRuleEngineDL efw = new BaseRuleEngineDL(ConfigurationManager.AppSettings[""], new TimeSpan(0,1,0,0));

            var rulesChunk = DB.GetWorkFlowRules(100);
            foreach (var flowRulesPoco in rulesChunk)
            {
                w3inputstream = new AntlrInputStream(flowRulesPoco.RuleXPath);
                var w3Lexer = new XPath1W3Lexer(w3inputstream);
                var w3Tokens = new CommonTokenStream(w3Lexer);
                var w3Parser = new XPath1W3Parser(w3Tokens) {BuildParseTree = true};
                var w3Tree = w3Parser.start();

                //w3tree.ToStringTree(w3parser);
                XPath1W3Visitor w3Visitor = new XPath1W3Visitor();

                flowRulesPoco.ConvertedCode = w3Visitor.Visit(w3Tree);

                //efw.InsertOrUpdateRule(flowRulesPoco.Map());
            }
        }

        private static void validationParse()
        {
            AntlrInputStream w3inputstream;

            var rulesChunk = DB.GetRuleValiations(100);
            foreach (var ruleValsPoco in rulesChunk)
            {
                w3inputstream = new AntlrInputStream(ruleValsPoco.xpath);
                var w3Lexer = new XPath1W3Lexer(w3inputstream);
                var w3Tokens = new CommonTokenStream(w3Lexer);
                var w3Parser = new XPath1W3Parser(w3Tokens) {BuildParseTree = true};
                var w3Tree = w3Parser.start();

                //w3tree.ToStringTree(w3parser);
                XPath1W3Visitor w3Visitor = new XPath1W3Visitor();

                ruleValsPoco.ConvertedCode = w3Visitor.Visit(w3Tree);

                w3inputstream = new AntlrFileStream(ruleValsPoco.xpathCondAlt);
                w3Lexer = new XPath1W3Lexer(w3inputstream);
                w3Tokens = new CommonTokenStream(w3Lexer);
                w3Parser = new XPath1W3Parser(w3Tokens) { BuildParseTree = true };
                w3Tree = w3Parser.start();

                //w3tree.ToStringTree(w3parser);
                w3Visitor = new XPath1W3Visitor();

                ruleValsPoco.ConvertedCondCode = w3Visitor.Visit(w3Tree);
            }
        }

        private static void Test()
        {
            //string inputXPATH = "/clp/pat[(@fname='LENA' and @lname='BRUCE') or (@fname='ROSA' and @lname='DANIELS')]";
            //string outputExpected = "clp.pat.Where(c17=>(c17.fname==\"LENA\" && c17.lname==\"BRUCE\") || (c17.fname==\"ROSA\" && c17.lname==\"DANIELS\"))";

            string inputXPATH = "/payer/@type[.='CH']";
            string outputExpected = "payer.type==\"CH\"";

            #region xpath1w3

            AntlrInputStream w3inputstream = new AntlrInputStream(inputXPATH);
            var w3lexer = new XPath1W3Lexer(w3inputstream);
            var w3tokens = new CommonTokenStream(w3lexer);
            var w3parser = new XPath1W3Parser(w3tokens);
            w3parser.BuildParseTree = true;
            var w3tree = w3parser.start();

            w3tree.ToStringTree(w3parser);
            //XPath1W3Visitor w3Visitor = new XPath1W3Visitor();
            XPath1W3VisistorLinkedList w3Visitor = new XPath1W3VisistorLinkedList();
            string convertTest = w3Visitor.Visit(w3tree);

            if (convertTest != outputExpected) Console.WriteLine("basic conversion failure");

            #endregion
        }
    }
}
