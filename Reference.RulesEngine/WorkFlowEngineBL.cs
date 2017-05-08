using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using RulesEngine.DAL;
using LawsonCS.Business.RulesEngine;
using LawsonCS.RulesEngine.Data;

namespace LawsonCS.WorkFlowEngine.RulesEngine
{
    public interface WorkflowState { }


    public class WorkFlowEngineBL
    {
        private readonly BaseRuleEngineBL _rulesBL;
        private readonly IRulesEngineDAL _rulesDAL;
        private List<RuleAppliedBase> _rules;
        private const string TableName = "WorkFlowRuleApplied";

        private readonly Dictionary<int, Func<WorkflowState, bool>> _codeFuncs = new Dictionary<int, Func<WorkflowState, bool>>();
        private readonly Dictionary<int, Func<WorkflowState, bool>> _conditionFuncs = new Dictionary<int, Func<WorkflowState, bool>>();


        public WorkFlowEngineBL(IRulesEngineDAL idal)
        {
            _rulesDAL = idal;

            _rulesBL = new BaseRuleEngineBL(_rulesDAL, TableName);

            RefreshRules();
        }

        private void RefreshRules()
        {
            _rules = new List<RuleAppliedBase>();
            _rules = _rulesBL.RefreshRules();

            CompilerResults results = _rulesBL.CompileRules(_rules);

            Type binaryFunction = results.CompiledAssembly.GetTypes()[0];

            foreach (RuleAppliedBase ruleAppliedBase in _rules)
            {
                //main code
                RuleCodeCache codeCache = _rulesDAL.DALCache.RuleCodes.FirstOrDefault(c => c.RuleCodeId == ruleAppliedBase.CodeId);
                if (codeCache == null)
                    throw new NullReferenceException("code cache not found during delegate building");

                MethodInfo r = binaryFunction.GetMethod(string.Format("Z_{0}_{1}_{2}", codeCache.CodeHash, codeCache.RuleCodeId, ruleAppliedBase.RuleAppliedId));
                if(r==null || string.IsNullOrWhiteSpace(r.Name))
                    throw new NullReferenceException("method name can't be null");

                var ruleRun1 = (Func<WorkflowState, bool>)Delegate.CreateDelegate(typeof(Func<WorkflowState, bool>), r);
                _codeFuncs.Add(ruleAppliedBase.CodeId, ruleRun1);

                //condition cod 
                if (ruleAppliedBase.ConditionCodeId == null) continue;
                codeCache = _rulesDAL.DALCache.RuleCodes.FirstOrDefault(c => c.RuleCodeId == ruleAppliedBase.ConditionCodeId);
                if (codeCache == null)
                    throw new NullReferenceException("condCode cache not found during delegate building");

                r = binaryFunction.GetMethod(string.Format("Z_{0}_{1}_", codeCache.CodeHash, codeCache.RuleCodeId));
                if (r==null || string.IsNullOrWhiteSpace(r.Name))
                    throw new NullReferenceException("method name can't be null");

                var ruleRun2 = (Func<WorkflowState, bool>)Delegate.CreateDelegate(typeof(Func<WorkflowState, bool>), r);
                _conditionFuncs.Add(ruleAppliedBase.ConditionCodeId.Value, ruleRun2);
            }
        }

        public RunRulesStatus<bool> RunRules(RuleAppliedFilter raf, WorkflowState input)
        {
            var param = new RunRulesParameters<WorkflowState, bool> { CondFuncs = _conditionFuncs, RuleFuncs = _codeFuncs, ContinueRunningAfterException = false, RunUntilFirstRuleHit = true };

            List<RuleAppliedBase> rulesToRun = _rules.Where(c => FilterRules(raf).Contains(c.RuleAppliedId)).ToList();
            
            RunRulesStatus<bool> retVal =  _rulesBL.RunRules<WorkflowState, bool>(rulesToRun, ref input, param);
             
            return retVal;
        }

        private List<int> FilterRules(RuleAppliedFilter raf)
        {
            IEnumerable<RuleAppliedBase> ruleQuery = _rules.Where(c => c.IsEnabled);

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
