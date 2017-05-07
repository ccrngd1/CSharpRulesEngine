//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.Remoting.Contexts;
//using System.Runtime.Remoting.Messaging;
//using Antlr4;
//using Antlr4.Runtime;
//using Antlr4.Runtime.Tree;
//using RoslynRuleEngineConsole.xpath1w3;


//namespace RoslynRuleEngineConsole
//{

//    internal class XPath1Visitor : XPath1BaseVisitor<string>
//    {
//        public override string VisitMain(XPath1Parser.MainContext context)
//        {
//            return base.VisitMain(context);
//        }
//    }

//    static class Program
//    {
//        //private static readonly string[] pathTokens = {"/", "//"};
//        //private static readonly string[] subExpStart = {"("};
//        //private static readonly string[] filterExpStart = {"["};
//        //private static readonly string[] subExpEnd = {")"};
//        //private static readonly string[] filterExpEnd = {"]"};
//        //private static readonly string[] expUnions = {"and", "or", "&&", "&", "||", "|"};
//        //private static readonly string[] attributeToken = {"@"};
//        //private static readonly string[] logicTokens = {"=",".=","!=",">","<",">=","<=","not"};
//        //private static readonly string[] valueToken = {"'", "\""};

//        static void Main(string[] args)
//        {
//            string inputXPATH = "/clp/pat[(@fname='LENA' and @lname='BRUCE') or (@fname='ROSA' and @lname='DANIELS')]";

//            #region xpath1w3
//            AntlrInputStream w3inputstream = new AntlrInputStream(inputXPATH);
//            var w3lexer = new XPath1W3Lexer(w3inputstream);
//            var w3tokens = new CommonTokenStream(w3lexer);
//            var w3parser = new XPath1W3Parser(w3tokens);
//            w3parser.BuildParseTree = true;
//            var w3tree = w3parser.start();

//            Console.WriteLine(w3tree.ToStringTree(w3parser));
//            XPath1W3Visitor w3visitor = new XPath1W3Visitor();
//            Console.WriteLine(w3visitor.Visit(w3tree));

//            #endregion

//            #region xpath1
//            AntlrInputStream ais = new AntlrInputStream(inputXPATH);

//            var lexer = new XPath1Lexer(ais);
//            var tokens = new CommonTokenStream(lexer);
//            var parser = new XPath1Parser(tokens);
//            parser.BuildParseTree = true;
//            var tree = parser.main();

//            Console.WriteLine(tree.ToStringTree(parser));
//            XPath1Visitor visitor = new XPath1Visitor();
//            Console.WriteLine(visitor.Visit(tree));

//            #endregion


//            var rulesChunk = DB.GetRuleValiations(100);
//            foreach (var ruleValsPoco in rulesChunk)
//            {
//                #region old parse attempt
//                //string newRule = "input";

//                //foreach (string pathNavElement in ruleValsPoco.xpathCondAlt.Split('/'))
//                //{
//                //    string toTest = pathNavElement;
//                //    if(string.IsNullOrWhiteSpace(toTest))continue;

//                //    if (toTest.Contains("["))
//                //    {
//                //        //samples!
//                //        // /clp/header[@seq="2" and not(@resubmitby)]
//                //        // 
//                //        //  
//                //        int navVSqualIndex = toTest.IndexOf("[");

//                //        string navigationPortion = toTest.Substring(0, navVSqualIndex);
//                //        string qualifierPortion = toTest.Substring(navVSqualIndex);

//                //        if (navigationPortion[0] == '@') navigationPortion = navigationPortion.Remove(0, 1);
//                //        if (navigationPortion == "ref") navigationPortion = "@" + navigationPortion;

//                //        newRule += "." + navigationPortion;

//                //        if (qualifierPortion[0] == '@') qualifierPortion = qualifierPortion.Remove(0, 1);
//                //        if (qualifierPortion == "ref") qualifierPortion = "@" + qualifierPortion;

//                //        newRule += "." + qualifierPortion;
//                //    }
//                //    else
//                //    {
//                //        if (toTest[0] == '@') toTest = toTest.Remove(0, 1);
//                //        newRule += "." + toTest;
//                //    }
//                //}
//                #endregion

//                //ruleValsPoco.ParseRuleValXpaths();
//            }
//        }

//        #region old parse attempt

//        //private static void ParseRuleValXpaths(this RuleValidationsPOCO poco)
//        //{
//        //    ParseXpathCond(poco);
//        //    ParseXpathAction(poco);
//        //}

//        //private static void ParseXpathCond(this RuleValidationsPOCO poco)
//        //{
//        //    var condCode = ParseXPath(poco.xpathCondAlt);
//        //}

//        //private static void ParseXpathAction(this RuleValidationsPOCO poco)
//        //{
//        //    var execCode = ParseXPath(poco.xpath);
//        //}

//        //private static string ParseXPath(string xpath, string currentPath="", string currentFieldAtt="", string currentValue="")
//        //{
//        //    int i = 0;

//        //    string convertedCode = "";

//        //    bool buildingPath = false;
//        //    bool buildingAttr = false;
//        //    bool buildingValue = false;

//        //    while (i < xpath.Length)
//        //    {
//        //        if (pathTokens.Contains(xpath[i].ToString()) && !buildingValue)
//        //        {
//        //            if (!string.IsNullOrWhiteSpace(currentPath)) currentPath += ".";

//        //            buildingPath = true;
//        //            buildingAttr = false;
//        //            buildingValue = false;
//        //        }
//        //        else if (subExpStart.Contains(xpath[i].ToString()) && !buildingValue)
//        //        {
//        //            var subCode = ParseXPath(ExtractSubString(xpath.Substring(i), subExpStart, subExpEnd)); //todo merge subcode into main statement
//        //            i = i + subCode.Length;
//        //        }
//        //        else if (filterExpStart.Contains(xpath[i].ToString()) && !buildingValue)
//        //        {
//        //            var subCode = ParseXPath(ExtractSubString(xpath.Substring(i), filterExpStart, filterExpEnd), currentPath, currentFieldAtt); //todo merge subcode into main statement
//        //            i = i + subCode.Length;
//        //        }
//        //        else if (expUnions.Contains(xpath[i].ToString()) && !buildingValue)
//        //        {
//        //            if (expUnions.Contains(xpath.Substring(i, 2)))
//        //            {
//        //                if (expUnions.Contains(xpath.Substring(i, 3)))
//        //                {
//        //                    i++; //we matched three chars instead of two, so bump here and  and below
//        //                }
//        //                i++; //we matched two chars instead of just one so increment up an extra one
//        //            }
//        //            var unionedCode = ParseXPath(xpath.Substring(i + 1), currentPath, currentFieldAtt);
//        //        }
//        //        else if (attributeToken.Contains(xpath[i].ToString()) && !buildingValue)
//        //        {
//        //            buildingAttr = true;
//        //            buildingValue = false;
//        //            buildingPath = false;

//        //            currentFieldAtt = "";

//        //            if (string.IsNullOrWhiteSpace(convertedCode))
//        //                convertedCode = currentPath;
//        //        }
//        //        else if (xpath[i].ToString() == " " && !buildingValue)
//        //        {
//        //            if (buildingPath) buildingPath = false;
//        //            if (buildingAttr) buildingAttr = false;
//        //        }
//        //        else if (valueToken.Contains(xpath[i].ToString()))
//        //        {
//        //            buildingValue = !buildingValue;
//        //        }
//        //        else //this should mean we are building up a path or attr
//        //        {
//        //            if (buildingAttr) currentFieldAtt += xpath[i];
//        //            if (buildingValue) currentValue += xpath[i];
//        //            if (buildingPath) currentPath += xpath[i];
//        //        }

//        //        i++;
//        //    }

//        //    return convertedCode;
//        //}

//        //private static string ExtractSubString(string path, string[] StartDelimiters, string[] EndDelimiters)
//        //{
//        //    int i = 0;

//        //    int totalStartTokens = 0;

//        //    string subExpression = "";

//        //    while (i < path.Length && totalStartTokens >= 0)
//        //    {
//        //        if (StartDelimiters.Contains(path[i].ToString())) totalStartTokens++;
//        //        if (EndDelimiters.Contains(path[i].ToString())) totalStartTokens--;

//        //        subExpression += path[i];

//        //        i++;
//        //    }

//        //    return subExpression;
//        //}
//        #endregion
//    }


//    public enum ActionTypeValidations
//    {
//        NodeCheck = 1,
//        ReqCheck = 2,
//        RejectMatch = 3,
//        PassMatch = 4,
//        DateValidation = 5
//    }

//    public class RuleValidationsPOCO
//    {
//        public ActionTypeValidations ActionTypeVal;
//        public string description;
//        public DateTime dtadded;
//        public DateTime dtupdated;
//        public string errMsg;
//        public string extraData;
//        public int RuleValId;
//        public int userId;
//        public string xmlSchema;
//        public string xpath;
//        public string xpathCondAlt;
//        public string ConvertedCode;
//    }
//}
