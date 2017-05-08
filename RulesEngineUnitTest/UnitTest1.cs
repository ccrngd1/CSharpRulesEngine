using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using RulesEngine.DAL;  
using System.Linq;
using CSCRuleEngineBL.CodeProject;
using LawsonCS.Business.RulesEngine;
using LawsonCS.Business.RulesEngine.Compiler;
using LawsonCS.RulesEngine;
using LawsonCS.RulesEngine.Data;
using TempRules = CSCRuleEngineBL.CodeProject.TempRules;

namespace RulesEngineUnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestDynamicLoad2()
        {
            CSCRuleEngineBL.CodeProject.DynamicLoad2 dl2 = new DynamicLoad2();

            dl2.Test();
        }

        [TestMethod]
        public void TestDynamicLoad2_2()
        {
            var prof = new DyanmicLoadGenericObject();
            var inst = new DyanmicLoadGenericObject();

            CSCRuleEngineBL.CodeProject.DynamicLoad2 dl2 = new DynamicLoad2();

            string s = null;
            using (StreamReader sr = new StreamReader("CodeTest.txt"))
            {
                s = sr.ReadToEnd();
            }

            var ret = dl2.Test2(s);

            List<TempRules> biggerList = new List<TempRules>();

            for (int i = 0; i < 1000; i++)
            {
                biggerList.AddRange(ret);
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var tempRulese in biggerList)
            {
                var claim1 = tempRulese.ruleRun1(prof);
                var claim2 = tempRulese.ruleRun1(inst);

                if(claim1==null && claim2==null)Console.WriteLine("Has to be one or the other");
            }
            sw.Stop();
            var t = sw.ElapsedMilliseconds;
        }

       

        #region Roslyn tests
        //[TestMethod]
        //public void TrivialTest()
        //{
        //    int answer = new RoslynRuleEngine.Test().TestMethod<int>();

        //    Assert.AreEqual(answer,4);
        //}

        //[TestMethod]
        //public void MockClaimStructTest()
        //{
        //    var inst = new InstC();
        //    var answer3 = new Test().RunTestClaimRules<InstC>(inst, "input.name =\"test\";");
        //}

        //[TestMethod]
        //public void ProfClaimTrivialRule()
        //{
        //    var profClaim = new clp {header = new header {batchid = "5"}};

        //    BaseRuleEngineDL efw = new BaseRuleEngineDL(ConfigurationManager.AppSettings["RulesEngine"]);
        //    var answer2 = new RoslynRuleEngine(efw).RunCLPRules(profClaim, "input.header.batchid = \"test\";");

        //    Assert.AreEqual(profClaim.header.batchid,"test");
        //    Assert.AreEqual(answer2.header.batchid, profClaim.header.batchid);
        //}

        //[TestMethod]
        //public void ProfClaimWFERuleTest()
        //{
        //    var profClaim = new clp { header = new header { batchid = "5" } };

        //    BaseRuleEngineDL efw = new BaseRuleEngineDL(ConfigurationManager.AppSettings["RulesEngine"]);
        //    var answer2 = new RoslynRuleEngine(efw).RunCLPConditionRules(profClaim, "if (input.header == null) return false; if (input.ins == null) return false; var matchA = input.header.seq; return input.ins.Where(a => a.seq == matchA).SelectMany(a => a.payer).Where(a => a != null && a.namematch != null && a.namematch == \"asdf\").Any();");

        //    Assert.AreEqual(profClaim.header.batchid, "test"); 
        //}

        //[TestMethod]
        //public void ProfClaimTrivialRuleViaGenericRunRuleMethod()
        //{
        //    var profClaim = new clp {header = new header {batchid = "5"}};

        //    BaseRuleEngineDL efw = new BaseRuleEngineDL(ConfigurationManager.AppSettings["RulesEngine"]);
        //    var answer2 = RoslynRuleEngine.RunRule(profClaim
        //        , new List<string> { "input.header.batchid = \"test\";"}
        //        , new List<string> { "Claims.Objects.XSD.CLP" }
        //        );

        //    Assert.AreEqual(profClaim.header.batchid, "test");
        //    Assert.AreEqual(answer2.header.batchid, profClaim.header.batchid);
        //}

        //[TestMethod]
        //public void TestWrapper()
        //{
        //    var profClaim = new clp {header = new header { custid = "605"}};

        //    List<clp> claims = new List<clp> {profClaim};
        //    claims.Where(c17 => (c17.pat.fname == "LENA" && c17.pat.lname == "BRUCE") || (c17.pat.fname == "ROSA" && c17.pat.lname == "DANIELS"));

        //    BaseRuleEngineDL efw = new BaseRuleEngineDL(ConfigurationManager.AppSettings["RulesEngine"]);
        //    var bl = new RoslynRuleEngine(efw);

        //    RuleAppliedFilter raf = new RuleAppliedFilter
        //    {
        //        RuleCategory = "Test",
        //    };

        //    Rules b = bl.GetEnabledRules(raf);

        //    profClaim.RunRules(, bl);

        //    Assert.IsTrue(profClaim.header.batchid == "test");
        //}
        #endregion
    }
} 
