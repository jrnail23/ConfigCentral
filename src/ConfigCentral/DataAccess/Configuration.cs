using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConfigCentral.Domain;

namespace ConfigCentral.DataAccess
{
    public class Configuration : Entity<ConfigurationIdentifier>, IEnumerable<KeyValuePair<string, string>>
    {
        private readonly string _environment;
        private readonly IDictionary<string, string> _configPairs;

        public Configuration(string environment,IEnumerable<KeyValuePair<string, string>> configPairs):base(new ConfigurationIdentifier(environment))
        {
            _environment = environment;
            _configPairs = configPairs.ToDictionary(x => x.Key, x => x.Value);
        }

        public string this[string key]
        {
            get { return _configPairs[key]; }
            set { _configPairs[key] = value; }
        }
        public string Environment
        {
            get { return _environment; }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _configPairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _configPairs).GetEnumerator();
        }
    }
}