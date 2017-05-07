using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirmed.Claims.Validator.RulesEngine;
using Zirmed.Claims.Workflow.RulesEngine;
using ZirMed.Architecture.Utilities.CommandLine;
using ZirMed.RulesEngine.Data;

namespace RoslynRuleEngineConsole
{
    public enum ActionTypeValidations
    {
        NodeCheck = 1,
        ReqCheck = 2,
        RejectMatch = 3,
        PassMatch = 4,
        DateValidation = 5
    }

    public static class HashExt
    {
        public static long KnuthHash(this string s)
        {
            return ZirMed.Claims.Utilities.General.ClaimsUtilities.CalculateKnuthHash(s);
        }
    }

    public static class POCOExt
    {
        public static ValidationRuleApplied Map(this RuleValidationsPOCO valRulePoco)
        {
            var ret = new ValidationRuleApplied
            {
                //AppId = valRulePoco.ConvertedCode.Contains("clp.") ? 14 : valRulePoco.ConvertedCode.Contains("cli.") ? 1 : 0,
                //ApplyOrder = valRulePoco.RuleValId,
            };

            return ret;
        }

        public static EditRuleApplied Map(this RuleEditPOCO editRulePoco)
        {
            var ret = new EditRuleApplied
            { 

            };
            return ret;
        }

        public static WorkFlowRuleApplied Map(this WorkFlowRulesPOCO wfRulePoco)
        {
            var ret = new WorkFlowRuleApplied
            {

            };
            return ret;
        }

        public static RuleAppliedBase Map(this RuleAppliedPOCO rulePoco)
        {
            var re = new RuleAppliedBase {};
            return re;
        }
    }

    public class RuleAppliedPOCO
    {
        
    }

    public class RuleEditPOCO
    {
        
    }

    public class RuleValidationsPOCO
    {
        public ActionTypeValidations ActionTypeVal;
        public string description;
        public DateTime dtadded;
        public DateTime dtupdated;
        public string errMsg;
        public string extraData;
        public int RuleValId;
        public int userId;
        public string xmlSchema;
        public string xpath;
        public string xpathCondAlt;
        public string ConvertedCode;
        public string ConvertedCondCode;
    }
    
    public class WorkFlowRulesPOCO
    {
        public int RuleId;
        public int UserId;
        public long AccountId;
        public DateTime DTCreated;
        public int WorkGroupID;
        public string RuleXPath;
        public string SerializedObject;
        public int RuleOrder;
        public bool Broken;
        public string ConvertedCode; 
    }


    public class ProcessArguments
    {
        [Argument(ArgumentType.AtMostOnce, ShortName = "", HelpText = "rule source")]
        public string src;

        //[Argument(ArgumentType.AtMostOnce, ShortName = "", HelpText = "The encryption key to use for encrypting the file.")]
        //public string key;
        //[Argument(ArgumentType.AtMostOnce, DefaultValue = false, ShortName = "", HelpText = "Indicates if the claim should be hidden after a rejection event is built")]
        //public bool hide;
        //[Argument(ArgumentType.AtMostOnce, DefaultValue = true, ShortName = "", HelpText = "Indicates whether the result file should be encrypted.")]
        //public bool encrypt;
        //[DefaultArgument(ArgumentType.MultipleUnique | ArgumentType.AtMostOnce, HelpText = "The customers to include when building responses.")]
        //public string[] custIDs;
        //[Argument(ArgumentType.AtMostOnce, DefaultValue = true, ShortName = "", HelpText = "Indicates whether to run for all customers by pulling parameters from database instead of commandline.")]
        //public bool loopall;
        //[Argument(ArgumentType.AtMostOnce, ShortName = "", HelpText = "The Original BatchID to process for")]
        //public string origbatchid;
    }
}
