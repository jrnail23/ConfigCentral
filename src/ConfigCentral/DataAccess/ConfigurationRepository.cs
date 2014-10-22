using ConfigCentral.Domain;

namespace ConfigCentral.DataAccess
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IConfigurationStore _configurationStore;

        public ConfigurationRepository(IConfigurationStore configurationStore)
        {
            _configurationStore = configurationStore;
        }

        public Configuration GetByEnvironment(string environment)
        {
            var configPairs = _configurationStore.GetConfigPairs(environment);

            return new Configuration(environment, configPairs);
        }

        public void Save(Configuration configuration)
        {
            _configurationStore.Persist(configuration.Environment, configuration);
        }
    }
}