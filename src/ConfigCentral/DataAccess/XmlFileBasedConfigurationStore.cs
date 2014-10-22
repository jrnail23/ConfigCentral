using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ConfigCentral.Domain;

namespace ConfigCentral.DataAccess
{
    public class XmlFileBasedConfigurationStore : IConfigurationStore
    {
        private readonly string _configDataFolder;

        public XmlFileBasedConfigurationStore(string configDataFolder)
        {
            _configDataFolder = configDataFolder;
        }

        public IEnumerable<KeyValuePair<string, string>> GetConfigPairs(string environmentName)
        {
            var doc = GetXml(environmentName);

            return doc.Root.Descendants("Parameter")
                .ToDictionary(p => p.Attribute("key")
                    .Value,
                    p => p.Attribute("value")
                        .Value);
        }

        public void Persist(string environmentName, IEnumerable<KeyValuePair<string, string>> valuePairs)
        {
            throw new System.NotImplementedException();
        }

        private XDocument GetXml(string environmentName)
        {
            var fileName = string.Format("{0}.xml", environmentName);
            var dataFilePath = Path.Combine(_configDataFolder, fileName);
            var doc = XDocument.Load(dataFilePath);
            return doc;
        }
    }
}