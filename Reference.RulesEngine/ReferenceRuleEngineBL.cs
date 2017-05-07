using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RulesEngine.DAL;
using LawsonCS.Business.RulesEngine;
using LawsonCS.RulesEngine.Data;

namespace LawsonCS.Claims.Reference.RulesEngine
{
    public class ReferenceRuleEngineBL<Tin, Tout>
    {
        private readonly BaseRuleEngineBL _rulesBL;
        private readonly IRulesEngineDAL _rulesDAL;
        private List<RuleAppliedBase> _rules;
        private const string _tableName = "ReferenceDesign_RuleApplied";

        private readonly Dictionary<int, Func<Tin, Tout>> _codeFuncs = new Dictionary<int, Func<Tin, Tout>>();  
        private readonly Dictionary<int, Func<Tin,bool>>  _conditionFuncs = new Dictionary<int, Func<Tin, bool>>();

        public ReferenceRuleEngineBL(IRulesEngineDAL idal)
        {
            _rulesDAL = idal;

            _rulesBL = new BaseRuleEngineBL(_rulesDAL, _tableName);

            RefreshRules();
        }

        private void RefreshRules()
        {
            var dtbl = _rulesBL.RefreshRules();

            _rules = new List<RuleAppliedBase>();

            foreach (System.Data.DataRow row in dtbl.Rows)
            {
                string subCId = row["RuleSubCategoryId"].ToString();
                string applyOrder = row["ApplyOrder"].ToString();

                _rules.Add(new RuleAppliedBase
                {
                    RuleCategoryId = int.Parse(row["RuleCategoryId"].ToString()),
                    RuleSubCategoryId = string.IsNullOrWhiteSpace(subCId) ? (int?) null : int.Parse(subCId),
                    ApplyOrder = string.IsNullOrWhiteSpace(applyOrder) ? (int?) null : int.Parse(applyOrder),
                    CodeId = int.Parse(row["MainCodeId"].ToString()),
                    ConditionCodeId = int.Parse(row["ConditionCodeId"].ToString()),
                    IsEnabled = bool.Parse(row["IsEnabled"].ToString()),
                    RuleAppliedId = int.Parse(row["id"].ToString()),
                    RuleCodeInTypeId = int.Parse(row["CodeInputTypeId"].ToString()),
                    RuleCodeOutTypeId = int.Parse(row["CodeOutputTypeId"].ToString()),
                    RuleCodeConditionInTypeId = int.Parse(row["ConditionCodeInputTypeId"].ToString()),
                    RuleCodeConditionOutTypeId = int.Parse(row["ConditionCodeOutputTypeId"].ToString())
                });
            }

            var results = _rulesBL.CompileRules(_rules);  

            Type binaryFunction = results.CompiledAssembly.GetTypes()[0];

            foreach (var ruleAppliedBase in _rules)
            {
                //main code
                var codeCache = _rulesDAL.DALCache.RuleCodes.FirstOrDefault(c => c.RuleCodeId == ruleAppliedBase.CodeId);
                if (codeCache == null) throw new NullReferenceException("code cache not found during delegate building");

                MethodInfo r = binaryFunction.GetMethod(string.Format("Z_{0}_{1}_{2}", codeCache.CodeHash, codeCache.RuleCodeId, ruleAppliedBase.RuleAppliedId));

                var ruleRun1 = (Func<Tin, Tout>) Delegate.CreateDelegate(typeof (Func<Tin, Tout>), r);
                _codeFuncs.Add(ruleAppliedBase.CodeId, ruleRun1);

                //condition cod 
                if (ruleAppliedBase.ConditionCodeId == null) continue;
                codeCache = _rulesDAL.DALCache.RuleCodes.FirstOrDefault(c => c.RuleCodeId == ruleAppliedBase.ConditionCodeId);
                if (codeCache == null) throw new NullReferenceException("condCode cache not found during delegate building");

                r = binaryFunction.GetMethod(string.Format("Z_{0}_{1}_", codeCache.CodeHash, codeCache.RuleCodeId));

                var ruleRun2 = (Func<Tin, bool>)Delegate.CreateDelegate(typeof(Func<Tin, bool>), r);
                _conditionFuncs.Add(ruleAppliedBase.ConditionCodeId.Value, ruleRun2);
            }
        }

        public RunRulesStatus<Tout> RunRules(RuleAppliedFilter raf, Tin input)
        {
            RunRulesParameters<Tin, Tout> param = new RunRulesParameters<Tin,Tout> {CondFuncs = _conditionFuncs, RuleFuncs = _codeFuncs, ContinueRunningAfterException = false, RunUntilFirstRuleHit = false};

            return _rulesBL.RunRules<Tin,Tout> (_rules.Where(c => FilterRules(raf).Contains(c.RuleAppliedId)).ToList(), ref input, param);
        }

        private List<int> FilterRules(RuleAppliedFilter raf)
        {
            var ruleQuery = _rules.Where(c => c.IsEnabled);

            if (raf.InType != null)
                ruleQuery = ruleQuery.Where(c => c.RuleCodeInTypeId == raf.InType.RuleCodeIOTypeId);

            if (raf.OutType != null)
                ruleQuery = ruleQuery.Where(c => c.RuleCodeOutTypeId == raf.OutType.RuleCodeIOTypeId);

            if (raf.RuleCategory != null)
                ruleQuery = ruleQuery.Where(c => c.RuleCategoryId == raf.RuleCategory.RuleCategoryId);

            if (raf.RuleSubCategory != null)
                ruleQuery = ruleQuery.Where(c => c.RuleSubCategoryId == raf.RuleSubCategory.RuleCategoryId);
            
            return ruleQuery.Select(c => c.RuleAppliedId).ToList();
        } 
    }
}
