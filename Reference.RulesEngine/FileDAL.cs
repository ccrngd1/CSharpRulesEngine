using RulesEngine.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawsonCS.RulesEngine.Data;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Reference.RulesEngine
{
    public static class BatchCompletionExtensions
    {
        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var xmlserializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlserializer.Serialize(writer, value);
                return stringWriter.ToString();
            }
        }

        public static T Deserialize<T>(this string value)
        {
            T retVal;
            var deserializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(value))
            {
                object obj = deserializer.Deserialize(reader);
                retVal = (T)obj;
                reader.Close();
            }

            return retVal;
        }
    }


    public class FileDAL : IRulesEngineDAL
    {
        private string _ruleDir;
        public FileDAL(string ruleDir, TimeSpan refreshFreqTimeSpan)
        {
            _ruleDir = ruleDir;
            DALCache = new Cache(this, refreshFreqTimeSpan);
        }

        public Cache DALCache {get;}

        public List<RuleCodeIOType> GetAvailabileRuleCodeTypes()
        {
            using (var fs = new StreamReader(System.IO.Path.Combine(_ruleDir, "RuleCodeTypes.xml")))
            {
                var s = fs.ReadToEnd();
                return s.Deserialize<List<RuleCodeIOType>>();
            }
        }

        public List<RuleCategory> GetAvailableCategories()
        {
            using (var fs = new StreamReader(System.IO.Path.Combine(_ruleDir, "Categories.xml")))
            {
                var s = fs.ReadToEnd();
                return s.Deserialize<List<RuleCategory>>();
            }
        }

        public List<RuleAppliedBase> GetEnabledRules(RuleAppliedFilter raf)
        {
            using (var fs = new StreamReader(System.IO.Path.Combine(_ruleDir, "Rules.xml")))
            {
                var s = fs.ReadToEnd();
                return s.Deserialize<List<RuleAppliedBase>>();
            }
        }

        public RuleAppliedBase GetRuleApplied(RuleAppliedFilter raf)
        {
            using (var fs = new StreamReader(System.IO.Path.Combine(_ruleDir, "RuleAppliedBase.xml")))
            {
                var s = fs.ReadToEnd();
                return s.Deserialize<RuleAppliedBase>();
            }
        }

        public void GetRuleCode(List<RuleCodeCache> rules)
        {
            throw new NotImplementedException();
        }

        public List<RuleCodeCache> GetRuleCodeMetaData(List<int> codeIds)
        {
            using (var fs = new StreamReader(System.IO.Path.Combine(_ruleDir, "RuleCodeMetaData.xml")))
            {
                var s = fs.ReadToEnd();
                return s.Deserialize<List<RuleCodeCache>>();
            }
        }

        public List<string> GetRuleppliedTables()
        {
            return null;
        }
    }
}
