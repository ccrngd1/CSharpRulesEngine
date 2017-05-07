using System;

namespace LawsonCS.RulesEngine.Data
{
    public class RuleCategory
    {
        public int RuleCategoryId;
        public string CategoryName;
        public int? ParentCategoryId;

        public override string ToString()
        {
            return CategoryName;
        }
    }

    public class RuleCodeCache
    {
        public int RuleCodeId;
        public int InputTypeId;
        public int OutputTypeId;
        public ulong CodeHash;
        public bool NeedsRefresh=true;
        public bool ExistsInCache;
        public string CodeText;
    }

    public class RuleCode : RuleCodeCache
    {
        public int CreatedByUserId;
        public string OriginalXpath;
        public long OriginalXpathHash;
    }

    public class RuleCodeIOType
    {
        public int RuleCodeIOTypeId;
        public string TypeName;
        public override string ToString()
        {
            return TypeName;
        }
    }

    public class RuleAppliedBase : IBaseRuleApplied
    {
        public int RuleAppliedId { get; set; }
        public bool IsEnabled { get; set; }
        public int RuleCategoryId { get; set; }
        public int? RuleSubCategoryId { get; set; }
        public decimal? ApplyOrder { get; set; }
        public int CodeId { get; set; }
        public int? ConditionCodeId { get; set; } 
        public int RuleCodeInTypeId { get; set; }
        public int RuleCodeOutTypeId { get; set; }
        public int? RuleCodeConditionInTypeId { get; set; }
        public int? RuleCodeConditionOutTypeId { get; set; }

        //public string Code { get; set; }
        //public string ConditionCode { get; set; }
        //public bool CodeNeedsRefresh { get; set; }
        //public bool CoditionCodeNeedsRefresh { get; set; }

        //Don't keep these because they are in the cache
        //public RuleCodeIOType RuleCodeInType { get; }
        //public RuleCodeIOType RuleCodeOutType { get; }
        //public RuleCodeIOType RuleCodeConditionInType { get; }
        //public RuleCodeIOType RuleCodeConditionOutType { get; }

        public bool IsInFilter(RuleAppliedFilter raf)
        {
            if (raf.InType != null && raf.InType.RuleCodeIOTypeId != RuleCodeInTypeId) return false;

            if (raf.OutType != null && raf.OutType.RuleCodeIOTypeId != RuleCodeOutTypeId) return false;

            if (raf.RuleCategory != null && raf.RuleCategory.RuleCategoryId != RuleCategoryId) return false;

            return true;
        }
    }
    
    public struct RulesDelegates<Tin, Tout>
    {
        public int codeId;
        public Func<Tin, Tout> RunRule;
    }

    public interface IBaseRuleApplied
    {
        int RuleAppliedId { get; set; }
        bool IsEnabled { get; set; }
        int RuleCategoryId { get;set; }
        int? RuleSubCategoryId { get; set; }
        decimal? ApplyOrder { get; set; }
        int CodeId { get; set; }
        int? ConditionCodeId { get; set; } 
    }
}
