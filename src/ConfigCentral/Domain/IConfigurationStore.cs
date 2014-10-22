using System.Collections.Generic;

namespace ConfigCentral.Domain
{
    public interface IConfigurationStore
    {
        IEnumerable<KeyValuePair<string, string>> GetConfigPairs(string environmentName);
        void Persist(string environmentName, IEnumerable<KeyValuePair<string, string>> valuePairs);
    }
}