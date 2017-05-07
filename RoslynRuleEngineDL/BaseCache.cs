using System;
using System.Collections.Generic;
using System.Linq;
using LawsonCS.RulesEngine.Data;

namespace RulesEngine.DAL
{
    public class Cache
    {
        private readonly IRulesEngineDAL _dal;

        private DateTime _lastRefreshAll;
        private DateTime _nextRefreshAll;

        private readonly TimeSpan _refreshFrequency;

        public Cache(IRulesEngineDAL iRulesDal, TimeSpan refreshFrequency)
        {
            _dal = iRulesDal;
            _refreshFrequency = refreshFrequency;
        }

        private void RefreshCache()
        {
            RefreshRuleCategories();
            RefreshRuleCodeIOTypes();

            _lastRefreshAll = DateTime.Now;
            _nextRefreshAll = _lastRefreshAll + _refreshFrequency;
        }

        #region rule cats
        private List<RuleCategory> _ruleCategory = new List<RuleCategory>();
        private DateTime _lastRefreshRulecategory;
        private DateTime _nextRefreshRulecategory;
        private readonly object _ruleCatLock = new object();
        public List<RuleCategory> RuleCategories
        {
            get
            {
                if (_ruleCategory == null || !_ruleCategory.Any()) RefreshRuleCategories();

                if (DateTime.Now > _nextRefreshRulecategory) RefreshRuleCategories();

                if (DateTime.Now > _nextRefreshAll) RefreshCache();

                return _ruleCategory;
            }
        }
        private void RefreshRuleCategories()
        {
            lock (_ruleCatLock)
            {
                if (DateTime.Now <= _nextRefreshRulecategory) return;

                _ruleCategory = _dal.GetAvailableCategories();

                _lastRefreshRulecategory = DateTime.Now;
                _nextRefreshRulecategory = _lastRefreshRulecategory + _refreshFrequency;
            }
        }
        #endregion

        #region rule code io types
        private readonly object _ruleCodeIoTypeLock = new object();
        private List<RuleCodeIOType> _ruleCodeIOTypes = new List<RuleCodeIOType>();
        private DateTime _lastRefreshRuleCodeIOTypes;
        private DateTime _nextRefreshRuleCodeIOTypes;
        private void RefreshRuleCodeIOTypes()
        {
            lock (_ruleCodeIoTypeLock)
            {
                if (DateTime.Now <= _nextRefreshRuleCodeIOTypes) return;

                _ruleCodeIOTypes = _dal.GetAvailabileRuleCodeTypes();

                _lastRefreshRuleCodeIOTypes = DateTime.Now;
                _nextRefreshRuleCodeIOTypes = _lastRefreshRuleCodeIOTypes + _refreshFrequency;
            }
        }
        public List<RuleCodeIOType> RuleCodeIOTypes
        {
            get
            {
                if (_ruleCodeIOTypes == null || !_ruleCodeIOTypes.Any()) RefreshRuleCodeIOTypes();

                if (DateTime.Now > _nextRefreshRuleCodeIOTypes) RefreshRuleCodeIOTypes();

                if (DateTime.Now > _nextRefreshAll) RefreshCache();

                return _ruleCodeIOTypes;
            }
        }
        #endregion

        #region rule code metadata
        private readonly object _ruleCodeLock = new object();
        private List<RuleCodeCache> _ruleCode = new List<RuleCodeCache>();
        private DateTime _lastRefreshRuleCode;
        private DateTime _nextRefreshRuleCode;

        public void RefreshRuleCodes(List<int> ruleIds=null, bool force=false)
        {
            lock (_ruleCodeLock)
            {
                if (DateTime.Now <= _nextRefreshRuleCode && !force) return;

                if (ruleIds != null && ruleIds.Any())
                {
                    foreach (var newRuleId in ruleIds.Where(c => !_ruleCode.Select(d => d.RuleCodeId).Contains(c)))
                    {
                        _ruleCode.Add(new RuleCodeCache
                        {
                            RuleCodeId = newRuleId,
                        });
                    }
                }

                _ruleCode = _dal.GetRuleCodeMetaData(_ruleCode.Select(c=>c.RuleCodeId).ToList());

                _lastRefreshRuleCode = DateTime.Now;
                _nextRefreshRuleCode = _lastRefreshRuleCode + _refreshFrequency;
            }
        }

        public List<RuleCodeCache> RuleCodes
        {
            get
            {
                if(_ruleCode==null || !_ruleCode.Any()) RefreshRuleCodes();

                if(DateTime.Now > _nextRefreshRuleCode) RefreshRuleCodes();

                if(DateTime.Now > _nextRefreshAll) RefreshRuleCodes();

                return _ruleCode;
            }
            set { _ruleCode = value; }
        }

        #endregion
    }
}
