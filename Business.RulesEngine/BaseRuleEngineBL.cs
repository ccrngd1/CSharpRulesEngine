using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using RulesEngine.DAL;
using LawsonCS.Business.RulesEngine.Compiler;
using LawsonCS.RulesEngine.Data;

namespace LawsonCS.Business.RulesEngine
{
    public class BaseRuleEngineBL
    {
        private readonly ICompilerBL _specificCompilerBL;
        private readonly IRulesEngineDAL _dal;

        private readonly RunRuleStatistics _ongoingRuleStats = new RunRuleStatistics(); 

        public string RulesTable { get; private set; }

        //public BaseRuleEngineBL(ICompilerBL comp, IRulesEngineDAL idal)
        //{
        //    _specificCompilerBL = comp;
        //    _dal = idal;
        //}

        private BaseRuleEngineBL(string compName, IRulesEngineDAL idal, string ruleTable)
        {
            _dal = idal;
            RulesTable = ruleTable;

            switch (compName)
            {
                case nameof(CSCCompilerBL):
                    _specificCompilerBL = new CSCCompilerBL(_dal, ruleTable);
                    break;
                //case nameof(RoslynRuleEngine):
                //    _specificCompilerBL=new RoslynRuleEngine(idal);
                //    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public RuleAppliedBase GetBaseRuleApplied(int ruleAppliedId, string ruleTable)
        {
            RuleAppliedBase ret = null;

            try
            {
                var raf = new RuleAppliedFilter
                {
                    RuleAppliedId = ruleAppliedId,
                    RuleTableName = ruleTable,
                };

                DataTable dt = _dal.GetRuleApplied(raf);

                if (dt == null) return null;

                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        var subCId = row["RuleSubCategoryId"].ToString();
                        var applyOrder = row["ApplyOrder"].ToString();
                        var condCodeId = row["ConditionCodeId"].ToString();
                        var condCodeInputId = row["ConditionCodeInputTypeId"].ToString();
                        var condCodeOutputId = row["ConditionCodeOutputTypeId"].ToString();

                        ret = new RuleAppliedBase
                        {
                            RuleCategoryId = int.Parse(row["RuleCategoryId"].ToString()),
                            RuleSubCategoryId = string.IsNullOrWhiteSpace(subCId) ? (int?) null : int.Parse(subCId),
                            ApplyOrder = string.IsNullOrWhiteSpace(applyOrder) ? (int?) null : int.Parse(applyOrder),
                            CodeId = int.Parse(row["MainCodeId"].ToString()),
                            ConditionCodeId = string.IsNullOrWhiteSpace(condCodeId)?(int?)null : int.Parse(condCodeId),
                            IsEnabled = bool.Parse(row["IsEnabled"].ToString()),
                            RuleAppliedId = int.Parse(row["id"].ToString()),
                            RuleCodeInTypeId = int.Parse(row["CodeInputTypeId"].ToString()),
                            RuleCodeOutTypeId = int.Parse(row["CodeOutputTypeId"].ToString()),
                            RuleCodeConditionInTypeId = string.IsNullOrWhiteSpace(condCodeInputId)?(int?)null : int.Parse(condCodeInputId),
                            RuleCodeConditionOutTypeId = string.IsNullOrWhiteSpace(condCodeOutputId)?(int?)null : int.Parse(condCodeOutputId)
                        };
                    }
                    catch (Exception innerE)
                    {
                        Console.WriteLine(innerE);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return ret;
        }

        public BaseRuleEngineBL(IRulesEngineDAL idal, string ruleTable)
            : this(ConfigurationManager.AppSettings["RuleEngineCompiler"], idal, ruleTable)
        {
        }

        public RunRulesStatus<Tout> RunRules<Tin, Tout>(IEnumerable<RuleAppliedBase> rules, ref Tin input,
            RunRulesParameters<Tin, Tout> runParameters)
        { 
            var ret = new RunRulesStatus<Tout> {RulesStatus = new Dictionary<int, RunRuleStatus>()};

            ret.LastRunStatistics.ObjectsRun++;

            var sw = new Stopwatch();
            foreach (RuleAppliedBase ruleAppliedBase in rules.OrderBy(c => c.ApplyOrder).ThenBy(c => c.RuleAppliedId))
            {
                sw.Start();
                try
                {
                    if (ruleAppliedBase.ConditionCodeId != null)
                    {
                        if (!runParameters.CondFuncs[ruleAppliedBase.ConditionCodeId.Value].Invoke(input)) continue;
                    }

                    ret.LastRunStatistics.TotalRulesRun ++;

                    ret.RulesStatus.Add(ruleAppliedBase.RuleAppliedId,
                        new RunRuleStatus() {Exceptions = null, CompletionStatus = RunRuleCompletionStatus.Completed});
                    ret.Result = runParameters.RuleFuncs[ruleAppliedBase.CodeId].Invoke(input);

                }
                catch (Exception ex)
                {
                    var tempRunRuleStat = new RunRuleStatus
                    {
                        Exceptions = new List<Exception> {ex},
                        CompletionStatus = RunRuleCompletionStatus.Failed
                    };

                    Exception iEx = ex.InnerException;
                    while (iEx != null)
                    {
                        tempRunRuleStat.Exceptions.Add(iEx);
                        iEx = iEx.InnerException;
                    }

                    if (ret.RulesStatus.ContainsKey(ruleAppliedBase.RuleAppliedId))
                        ret.RulesStatus[ruleAppliedBase.RuleAppliedId] = tempRunRuleStat;
                    else
                        ret.RulesStatus.Add(ruleAppliedBase.RuleAppliedId, tempRunRuleStat);
                }
                sw.Stop();
                sw.Reset();
                ret.LastRunStatistics.TotalRunTime += sw.Elapsed;

                if (ret.RulesStatus[ruleAppliedBase.RuleAppliedId].HasException &&
                    !runParameters.ContinueRunningAfterException)
                    break;
            } 

            _ongoingRuleStats.ObjectsRun += ret.LastRunStatistics.ObjectsRun;
            _ongoingRuleStats.TotalRulesRun += ret.LastRunStatistics.TotalRulesRun;
            _ongoingRuleStats.TotalRunTime += ret.LastRunStatistics.TotalRunTime;

            ret.GlobalRunStatistics = _ongoingRuleStats;

            return ret;
        }

        public RunRulesStatus<T> RunRules<T>(List<RuleAppliedBase> rules, ref T input,
            RunRulesParameters<T, T> runParameters)
        { 
            var ret = new RunRulesStatus<T> {RulesStatus = new Dictionary<int, RunRuleStatus>()};

            ret.LastRunStatistics.ObjectsRun++;

            T runningInput = input;

            var sw = new Stopwatch();
            foreach (RuleAppliedBase ruleAppliedBase in rules.OrderBy(c => c.ApplyOrder).ThenBy(c => c.RuleAppliedId))
            {
                sw.Start();
                try
                {
                    if (ruleAppliedBase.ConditionCodeId != null)
                    {
                        if (!runParameters.CondFuncs[ruleAppliedBase.ConditionCodeId.Value].Invoke(input)) continue;
                    }

                    ret.LastRunStatistics.TotalRulesRun++;

                    ret.RulesStatus.Add(ruleAppliedBase.RuleAppliedId,
                        new RunRuleStatus() {Exceptions = null, CompletionStatus = RunRuleCompletionStatus.Completed});
                    ret.Result = runParameters.RuleFuncs[ruleAppliedBase.CodeId].Invoke(runningInput);

                    //since the input/output is the same, make sure we set the running input and the actualy input to the result of the rule
                    //this will do 2 things
                    //1) ensure the input is updated and passed to the subseqeunt rule call
                    //2) ensure if the in put is checked by the caller, it is updated to match the result that we are passing out - this should be the case anyway, but ew want to enforce it
                    runningInput = ret.Result;
                    input = ret.Result;
                }
                catch (Exception ex)
                {
                    var tempRunRuleStat = new RunRuleStatus
                    {
                        CompletionStatus = RunRuleCompletionStatus.Failed
                    };

                    var iEx = ex.InnerException;
                    while (iEx != null)
                    {
                        tempRunRuleStat.Exceptions.Add(iEx);
                        iEx = iEx.InnerException;
                    }

                    ret.RulesStatus[ruleAppliedBase.RuleAppliedId] = tempRunRuleStat;
                }

                sw.Stop();
                sw.Reset();
                ret.LastRunStatistics.TotalRunTime += sw.Elapsed;

                if (ret.RulesStatus[ruleAppliedBase.RuleAppliedId].HasException &&
                    !runParameters.ContinueRunningAfterException)
                    break;
            } 

            _ongoingRuleStats.ObjectsRun += ret.LastRunStatistics.ObjectsRun;
            _ongoingRuleStats.TotalRulesRun += ret.LastRunStatistics.TotalRulesRun;
            _ongoingRuleStats.TotalRunTime += ret.LastRunStatistics.TotalRunTime;

            ret.GlobalRunStatistics = _ongoingRuleStats;

            return ret;
        }

        public RunRulesStatus<bool> RunRules<T>(IEnumerable<RuleAppliedBase> rules, ref T input,
            RunRulesParameters<T, bool> runParameters)
        {
            var ret = new RunRulesStatus<bool> {RulesStatus = new Dictionary<int, RunRuleStatus>()};

            ret.LastRunStatistics.ObjectsRun++;

            var sw = new Stopwatch();
            foreach (RuleAppliedBase ruleAppliedBase in rules.OrderBy(c => c.ApplyOrder).ThenBy(c => c.RuleAppliedId))
            {
                sw.Start();
                try
                {
                    if (ruleAppliedBase.ConditionCodeId != null)
                    {
                        if (!runParameters.CondFuncs[ruleAppliedBase.ConditionCodeId.Value].Invoke(input)) continue;
                    }

                    ret.LastRunStatistics.TotalRulesRun++;

                    ret.RulesStatus.Add(ruleAppliedBase.RuleAppliedId,
                        new RunRuleStatus() {Exceptions = null, CompletionStatus = RunRuleCompletionStatus.Completed});
                    ret.Result = runParameters.RuleFuncs[ruleAppliedBase.CodeId].Invoke(input);
                }
                catch (Exception ex)
                {
                    var tempRunRuleStat = new RunRuleStatus
                    {
                        Exceptions = new List<Exception> {ex},
                        CompletionStatus = RunRuleCompletionStatus.Failed
                    };

                    var iEx = ex.InnerException;
                    while (iEx != null)
                    {
                        tempRunRuleStat.Exceptions.Add(iEx);
                        iEx = iEx.InnerException;
                    }

                    ret.RulesStatus.Add(ruleAppliedBase.RuleAppliedId, tempRunRuleStat);
                }
                sw.Stop();
                sw.Reset();
                ret.LastRunStatistics.TotalRunTime += sw.Elapsed;

                //did rule have an exception? are we supposed to quit if we hit excpetions?
                if (ret.RulesStatus[ruleAppliedBase.RuleAppliedId].HasException &&
                    !runParameters.ContinueRunningAfterException)
                    break;

                //did we get a true result? are we supposed to run until we hit the first code that returns true?
                if (ret.Result && runParameters.RunUntilFirstRuleHit)
                    break;
            }

            _ongoingRuleStats.ObjectsRun += ret.LastRunStatistics.ObjectsRun;
            _ongoingRuleStats.TotalRulesRun += ret.LastRunStatistics.TotalRulesRun;
            _ongoingRuleStats.TotalRunTime += ret.LastRunStatistics.TotalRunTime;

            ret.GlobalRunStatistics = _ongoingRuleStats;

            return ret;
        }

        public DataTable RefreshRules()
        {
            return _specificCompilerBL.RefreshRules();
        }

        public CompilerResults CompileRules(List<RuleAppliedBase> rules)
        {
            return _specificCompilerBL.CompileRules(rules);
        }
    }

    public class RunRulesStatus<T>
    {
        public T Result;
        public Dictionary<int, RunRuleStatus> RulesStatus;
        public RunRuleStatistics GlobalRunStatistics = new RunRuleStatistics();
        public readonly RunRuleStatistics LastRunStatistics = new RunRuleStatistics();

        public RunRulesStatus()
        {
            RulesStatus = new Dictionary<int, RunRuleStatus>();
        }
    }

    public class RunRuleStatistics
    {
        public int ObjectsRun;
        public int TotalRulesRun;
        public TimeSpan TotalRunTime;
    }

    public class RunRuleStatus
    {
        public List<Exception> Exceptions = new List<Exception>();
        public RunRuleCompletionStatus CompletionStatus = RunRuleCompletionStatus.Unknown;

        public bool HasException
        {
            get { return Exceptions != null && Exceptions.Any(); }
        }
    }

    public enum RunRuleCompletionStatus
    {
        Unknown = 0,
        Completed = 1,
        Failed = 2,
    }

    public class RunRulesParameters<T> : RunRulesParameters<T, T>
    {
    }

    public class RunRulesParameters<TIn, TOut>
    {
        public Dictionary<int, Func<TIn, TOut>> RuleFuncs;
        public Dictionary<int, Func<TIn, bool>> CondFuncs;
        public bool ContinueRunningAfterException;
        public bool RunUntilFirstRuleHit;

        public RunRulesParameters()
        {
            ContinueRunningAfterException = false;
            RuleFuncs = new Dictionary<int, Func<TIn, TOut>>();
            CondFuncs = new Dictionary<int, Func<TIn, bool>>();
            RunUntilFirstRuleHit = false;
        }
    }
} ;
