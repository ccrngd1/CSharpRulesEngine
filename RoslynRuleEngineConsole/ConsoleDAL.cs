using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq; 
using ZirMed.RulesEngine.Data;

namespace RoslynRuleEngineConsole
{
    internal static class DB
    {
        private static int lastValIdRead = 0;

        internal static List<RuleValidationsPOCO> GetRuleValiations(int toTake)
        {
            string sqlGetRuleValidationsForConversion =
               string.Format("SELECT * FROM rules.dbo.RuleValidations WITH(NOLOCK) left JOIN rules.dbo.XPathTargetFields ON XPathTargetFields.XPathTargetFieldsID = RuleValidations.XPathTargetFieldsID left JOIN rules.dbo.ErrMsg ON ErrMsg.ErrMsgID = RuleValidations.ErrMsgID where RuleValid > {0} ORDER BY RuleValID", lastValIdRead);

            List<RuleValidationsPOCO> ret = new List<RuleValidationsPOCO>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DSN"]))
            using (SqlCommand cmd = new SqlCommand(sqlGetRuleValidationsForConversion, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                int i = 0;
                while (reader.Read() && i++ < toTake)
                {
                    RuleValidationsPOCO r = new RuleValidationsPOCO
                    {
                        ActionTypeVal = (ActionTypeValidations)int.Parse(reader["ActionTypeValID"].ToString()),
                        description = reader["Description"].ToString(),
                        dtadded = DateTime.Parse(reader["DtAdded"].ToString()),
                        dtupdated = DateTime.Parse(reader["DtUpdated"].ToString()),
                        errMsg = reader["Msg"].ToString(),
                        extraData = reader["ExtraData"].ToString(),
                        RuleValId = int.Parse(reader["RuleValId"].ToString()),
                        userId = int.Parse(reader["UsrID"].ToString()),
                        xmlSchema = reader["xmlSchemaID"].ToString() == "1" ? "CLP" : reader["xmlSchemaID"].ToString() == "2" ? "CLI" : null,
                        xpath = reader["xpath"].ToString(),
                        xpathCondAlt = reader["xpathCondAlt"].ToString()
                    };
                    ret.Add(r);
                } 
            }

            lastValIdRead = ret.Max(c => c.RuleValId);

            return ret;
        }

        private static int lastWFIdRead = 0;

        internal static List<WorkFlowRulesPOCO> GetWorkFlowRules(int toTake)
        {
            string sqlGetRuleValidationsForConversion =
               string.Format("SELECT * FROM zch.workcenter.workflowRule WITH(NOLOCK) where ruleid > {0} ORDER BY RuleID", lastWFIdRead);
            
            List<WorkFlowRulesPOCO> ret = new List<WorkFlowRulesPOCO>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DSN"]))
            using (SqlCommand cmd = new SqlCommand(sqlGetRuleValidationsForConversion, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                int i = 0;
                while (reader.Read() && i++ < toTake)
                {
                    WorkFlowRulesPOCO r = new WorkFlowRulesPOCO
                    {
                        RuleId = int.Parse(reader["ruleid"].ToString()),
                        UserId = int.Parse(reader["usrid"].ToString()),
                        AccountId = int.Parse(reader["AccountId"].ToString()),
                        DTCreated = DateTime.Parse(reader["DTCreated"].ToString()),
                        WorkGroupID = int.Parse(reader["WorkGroupID"].ToString()),
                        RuleXPath = reader["RuleXPath"].ToString(),
                        SerializedObject = reader["SerializedObject"].ToString(),
                        RuleOrder = int.Parse(reader["RuleOrder"].ToString()),
                        Broken = reader["Broken"].ToString() == "0",
                    };
                    
                    if (r.RuleXPath.StartsWith("/*[(")) 
                        r.RuleXPath = r.RuleXPath.Substring(4, r.RuleXPath.Length-4-4); 

                    ret.Add(r);
                }
            }

            lastValIdRead = ret.Max(c => c.RuleId);
            return ret;
        }

        internal static void SaveCode(List<RuleCode> rules)
        {
            foreach (var ruleCode in rules)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DSN"]))
                using (SqlCommand command = new SqlCommand("[RulesEngine].spa_InsertNewCode", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameterCollection parameters = command.Parameters;

                    parameters.Add("@InputTypeId", SqlDbType.VarChar, 50).Value = ruleCode.InputTypeId;
                    parameters.Add("@OutputTypeId", SqlDbType.VarChar, 50).Value = ruleCode.OutputTypeId;
                    parameters.Add("@UsrId", SqlDbType.VarChar, 50).Value = ruleCode.CreatedByUserId;
                    parameters.Add("@CodeText", SqlDbType.VarChar, 50).Value = ruleCode.CodeText;
                    parameters.Add("@CodeHash", SqlDbType.VarChar, 50).Value = ruleCode.CodeHash;
                    parameters.Add("@OriginalXPathHash", SqlDbType.VarChar, 50).Value = ruleCode.OriginalXpathHash;
                    parameters.Add("@OriginalXPath", SqlDbType.VarChar, 50).Value = ruleCode.OriginalXpath; 

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
