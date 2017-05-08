using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using LawsonCS.RulesEngine.Data;

namespace RulesEngine.DAL
{
    public interface ICompilerBL
    {
        //TOut RunRules<TOut, TIn>(List<RuleAppliedBase> rules, TIn input);
        List<RuleAppliedBase> RefreshRules();
        CompilerResults CompileRules(List<RuleAppliedBase> rules);
        string CurrentRuleTable();
    }


    public interface IRulesEngineDAL
    {
        List<RuleCategory> GetAvailableCategories();
        List<RuleCodeIOType> GetAvailabileRuleCodeTypes();
        List<RuleAppliedBase> GetEnabledRules(RuleAppliedFilter raf);
        Cache DALCache { get; }
        List<RuleCodeCache> GetRuleCodeMetaData(List<int> codeIds );
        void GetRuleCode(List<RuleCodeCache> rules);
        RuleAppliedBase GetRuleApplied(RuleAppliedFilter raf);
        List<string> GetRuleppliedTables();
    }
}
