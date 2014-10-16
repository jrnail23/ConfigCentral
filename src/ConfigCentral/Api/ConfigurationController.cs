using System.Configuration;
using System.Web.Http;
using ConfigCentral.DataAccess;
using ConfigCentral.Domain;

namespace ConfigCentral.Api
{
    [RoutePrefix("api")]
    public class ConfigurationController : ApiController
    {
        readonly IConfigurationStore _store;

        public ConfigurationController():this(new XmlFileBasedConfigurationStore(ConfigurationManager.AppSettings["XmlDataFolder"]))
        {
            
        }

        public ConfigurationController(IConfigurationStore store)
        {
            _store = store;
        }

        [Route("Configs/{application}/{version}/{environment}")]
        public IHttpActionResult Get(string application, string version, string environment)
        {
            var result = _store.GetConfigurationData(application, version, environment);
            return Ok(result);
        }
    }
}