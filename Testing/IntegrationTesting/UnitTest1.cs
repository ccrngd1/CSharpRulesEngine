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
using LawsonCS.Reference.RulesEngine;
using LawsonCS.WorkFlowEngine.RulesEngine;
using LawsonCS.RulesEngine;
using LawsonCS.RulesEngine.Data;
using TempRules = CSCRuleEngineBL.CodeProject.TempRules;

namespace IntegrationTesting
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void TestBaseRuleEngineWrapper1()
        {
            BaseRuleEngineDL dl = new BaseRuleEngineDL(ConfigurationManager.AppSettings["RulesEngine"], new TimeSpan(0, 0, 1, 0));

            var re = new ReferenceRuleEngineBL<DyanmicLoadGenericObject, DyanmicLoadGenericObject>(dl);
        }

        [TestMethod]
        public void TestCSCCompileRules()
        {
            BaseRuleEngineDL dl = new BaseRuleEngineDL(ConfigurationManager.AppSettings["RulesEngine"], new TimeSpan(0, 0, 1, 0));

            CSCCompilerBL target = new CSCCompilerBL(dl, "ReferenceDesign_RuleApplied");

            List<RuleAppliedBase> rules = new List<RuleAppliedBase>
            {
                new RuleAppliedBase
                {
                    RuleCategoryId = 1,
                    ApplyOrder = 2,
                    //Code = "string s = \"test\"; return s;",
                    IsEnabled = true,
                    RuleSubCategoryId = 3,
                    ConditionCodeId = 4,
                    RuleAppliedId = 5,
                    CodeId = 6,
                    //ConditionCode = "if(1==1)",
                }
            };

            PrivateObject obj = new PrivateObject(target);
            var retVal = obj.Invoke("CompileRules", rules);

        }


        [TestMethod]
        public void TestReferenceDesignRule()
        {
            try
            {
                BaseRuleEngineDL rulesDL =
                     new BaseRuleEngineDL(ConfigurationManager.AppSettings["RulesEngine"], new TimeSpan(0, 0, 5, 0));
                var rulesBL = new ReferenceRuleEngineBL<DyanmicLoadGenericObject, DyanmicLoadGenericObject>(rulesDL);

                DyanmicLoadGenericObject claim = new DyanmicLoadGenericObject();

                RuleAppliedFilter raf = new RuleAppliedFilter
                {
                    RuleCategoryId = 3,
                };

                var output = rulesBL.RunRules(raf, claim);

                Assert.IsTrue(output.Result != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
