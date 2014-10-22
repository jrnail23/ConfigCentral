using ConfigCentral.Domain.Model;

namespace ConfigCentral.DataAccess
{
    public class ConfigurationIdentifier : Identifier{
        private readonly string _environment;

        public ConfigurationIdentifier(string environment)
        {
            _environment = environment;
        }

        protected override object Value
        {
            get { return _environment; }
        }
    }
}