using System;
using System.Collections.Generic; 
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LawsonCS.RulesEngine.Data;  

namespace RulesEngine.DAL
{
    public static class DALExtension
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable, string tableName, string columnName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add(columnName);
            dt.Columns[columnName].DataType = typeof(T);

            foreach (object obj in enumerable)
            {
                var val = Convert.ChangeType(obj, typeof(T));
                dt.Rows.Add(val);
            }

            return dt;
        }
    }

    public class BaseRuleEngineDL : IRulesEngineDAL
    {

        private readonly string _connString;

        public Cache DALCache { get; }

        public BaseRuleEngineDL(string connectionString, TimeSpan refreshFreqTimeSpan)
        {
            _connString = connectionString;
            DALCache =new Cache(this, refreshFreqTimeSpan);
        }

        public void GetRuleCode(List<RuleCodeCache> rules)
        {
            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("RulesEngine.spa_GetCodeTextById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 360;

                conn.Open();

                cmd.Parameters.Add("@CodeIds", SqlDbType.Structured).Value = rules.Select(c=>c.RuleCodeId).ToDataTable("CodeIds", "Values");

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        RuleCodeCache temp = rules.FirstOrDefault(c => c.RuleCodeId == int.Parse(reader["RuleCodeId"].ToString()));

                        if (temp == null) continue;

                        temp.CodeHash = (ulong)long.Parse(reader["CodeHash"].ToString());
                        temp.InputTypeId = int.Parse(reader["InputTypeId"].ToString());
                        temp.OutputTypeId = int.Parse(reader["OutputTypeId"].ToString());
                        temp.CodeText = reader["CodeText"].ToString(); 
                    }
                }

                conn.Close();
            }
        } 

        public List<RuleCodeCache> GetRuleCodeMetaData(List<int> codeIds)
        {
           var ret = new List<RuleCodeCache>();

            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("RulesEngine.spa_GetCodeMetaData", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 360;

                conn.Open();

                cmd.Parameters.Add("@RuleIds", SqlDbType.Structured).Value = codeIds.ToDataTable("CodeIds", "Values");

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var temp = new RuleCodeCache
                        {
                            RuleCodeId = int.Parse(reader["RuleCodeId"].ToString()),
                            CodeHash= (ulong)long.Parse(reader["CodeHash"].ToString()),
                            InputTypeId = int.Parse(reader["InputTypeId"].ToString()),
                            OutputTypeId = int.Parse(reader["OutputTypeId"].ToString())
                        };

                        ret.Add(temp);
                    }
                }

                conn.Close();
            }

            return ret;
        }

        public List<RuleCodeIOType> GetAvailabileRuleCodeTypes()
        {
            List<RuleCodeIOType> ret = new List<RuleCodeIOType>();
            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("RulesEngine.spa_GetCodeIOTypes", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 360;

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var tempCat = new RuleCodeIOType
                        {
                            RuleCodeIOTypeId = int.Parse(reader["RuleCodeIOTypeId"].ToString()),
                            TypeName = reader["TypeName"].ToString(),
                        };

                        ret.Add(tempCat);
                    }
                }

                conn.Close();
            }

            return ret;
        }
        
        public List<RuleCategory> GetAvailableCategories()
        {
            List<RuleCategory> ret = new List<RuleCategory>();
            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("RUlesEngine.spa_GetRulesCategories", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 360;

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    { 
                        int? parentCategoryId = string.IsNullOrWhiteSpace(reader["ParentCategoryId"].ToString()) ? (int?)null :int.Parse(reader["ParentCategoryId"].ToString());

                        var tempCat = new RuleCategory
                        {
                            ParentCategoryId = parentCategoryId,
                            CategoryName = reader["CategoryName"].ToString(),
                            RuleCategoryId = int.Parse(reader["RuleCategoryId"].ToString())
                        };

                        ret.Add(tempCat);
                    }
                }

                conn.Close();
            }

            return ret;
        }
        
        public DataTable GetEnabledRules(RuleAppliedFilter raf )
        {
            if(raf==null) raf = new RuleAppliedFilter();

            var ret = new DataTable(); 

            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("RulesEngine.spa_GetRulesWithPreConditions", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 360;

                if(raf.InType!=null)
                    cmd.Parameters.Add("@inTypeId", SqlDbType.VarChar).Value = raf.InType;
                if (raf.OutType!=null)
                    cmd.Parameters.Add("@outTypeId", SqlDbType.VarChar).Value = raf.OutType;
                if (raf.RuleCategory!=null)
                    cmd.Parameters.Add("@categoryId", SqlDbType.VarChar).Value = raf.RuleCategory;
                if (raf.RuleSubCategory!=null)
                    cmd.Parameters.Add("@subCategoryId", SqlDbType.VarChar).Value = raf.RuleSubCategory;

                cmd.Parameters.Add("@ruleAppliedTableName", SqlDbType.VarChar).Value = raf.RuleTableName;

                conn.Open();

                using (var sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(ret);
                }
            }

            return ret;
        } 

        public DataTable GetRuleApplied(RuleAppliedFilter raf)
        {
            if (raf == null) return null;
            if (raf.RuleAppliedId == null | raf.RuleAppliedId <= 0) return null;
            if (raf.RuleTableName == null) return null;

            var ret = new DataTable();

            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("RulesEngine.spa_GetRuleApplied", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 360;

                if (raf.RuleAppliedId != null)
                    cmd.Parameters.Add("@ruleAppliedId", SqlDbType.VarChar).Value = raf.RuleAppliedId;

                cmd.Parameters.Add("@ruleAppliedTableName", SqlDbType.VarChar).Value = raf.RuleTableName;

                conn.Open();

                using (var sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(ret);
                }
            }

            return ret;
        }

        public List<string> GetRuleppliedTables()
        {
            var ret = new List<string>();

            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("RulesEngine.spa_GetRuleAppliedTableNames", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 360;  

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        ret.Add(reader["TABLE_NAME"].ToString());
                    }
                }
            }

            return ret;

        } 
    } 
}
