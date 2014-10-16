using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ConfigCentral.Domain;

namespace ConfigCentral.DataAccess
{
    public class XmlFileBasedConfigurationStore : IConfigurationStore
    {
        readonly string _configDataFolder;

        public XmlFileBasedConfigurationStore(string configDataFolder)
        {
            _configDataFolder = configDataFolder;
        }

        public IDictionary<string, string> GetConfigurationData(string application,
            string appVersion,
            string environmentName)
        {
            var fileName = string.Format("{0}ConfigParameters.xml", environmentName);
            var dataFilePath = Path.Combine(_configDataFolder, fileName);
            var doc = XDocument.Load(dataFilePath);

            return doc.Root.Descendants("Parameter")
                .ToDictionary(p => p.Attribute("key")
                    .Value,
                    p => p.Attribute("value")
                        .Value);

        }
    }
}