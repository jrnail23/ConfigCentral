using System.Collections.Generic;

namespace ConfigCentral.Domain
{
    public interface IConfigurationStore
    {
        IDictionary<string, string> GetConfigurationData(string application, string appVersion, string environmentName);
    }
}