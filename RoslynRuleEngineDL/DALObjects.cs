namespace LawsonCS.RulesEngine.Data
{
    public class RuleAppliedFilter
    {
        public string RuleTableName;
        public RuleCategory RuleCategory { get;}
        public RuleCategory RuleSubCategory { get; }
        public int? RuleCategoryId { private get; set; }
        public int? RuleSubCategoryId { private get; set; }

        public RuleCodeIOType InType { get; }
        public RuleCodeIOType OutType { get; }
        public int? OutTypeId { private get; set; }
        public int? InTypeId { private get; set; }
        public int? RuleAppliedId { get; set; }
    }
}
