using System.Configuration;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using ConfigCentral.DataAccess;

namespace ConfigCentral.Api
{
    [RoutePrefix("api")]
    public class ConfigurationController : ApiController
    {
        private readonly IConfigurationRepository _repository;

        public ConfigurationController()
            : this(
                new ConfigurationRepository(
                    new XmlFileBasedConfigurationStore(ConfigurationManager.AppSettings["XmlDataFolder"]))) {}

        public ConfigurationController(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        [Route("configs/{environment}")]
        public IHttpActionResult Get(string environment)
        {
            var result = _repository.GetByEnvironment(environment);
            return Ok(result);
        }

        [Route("configs/{environment}/{parameterKey}")]
        public IHttpActionResult Get(string environment,string parameterKey)
        {
            var result = _repository.GetByEnvironment(environment)[parameterKey];
            return Ok(result);
        }

        [Route("configs/{environment}/{parameterKey}")]
        public IHttpActionResult Put(string environment, string parameterKey,[FromBody] string value)
        {
            var result = _repository.GetByEnvironment(environment)[parameterKey];
            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }
    }
}