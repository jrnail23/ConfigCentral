using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using ConfigCentral.DomainModel;

namespace ConfigCentral.WebApi.Resources.Applications
{
    public class ApplicationsController : ApiController
    {
        private readonly ApplicationsStore _appsStore;

        public ApplicationsController() : this(new ApplicationsStore()) {}

        public ApplicationsController(ApplicationsStore appsStore)
        {
            _appsStore = appsStore;
        }

        [Route("applications/{name}")]
        public IHttpActionResult Get([FromUri] string name)
        {
            try
            {
                var model = _appsStore.FindByName(name);
                return Ok(model);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            
        }

        [Route("applications")]
        public IHttpActionResult Get()
        {
            try
            {
                var model = _appsStore.All();
                return Ok(model);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }

        }

        [Route("applications")]
        public IHttpActionResult Post([FromBody] ApplicationState application)
        {
            var appEntity = new Application(application.Name);

            try
            {
                _appsStore.Add(appEntity);
            }
            catch (Exception e)
            {
                // TODO: refactor to a strongly typed exception, extract response mapping into an exception filter or something
                var entity = new
                {
                    ErrorDescription = e.Message
                };
                var msg = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new ObjectContent<object>(entity, new JsonMediaTypeFormatter())
                };
                msg.Headers.Location = new Uri("/applications/" + application.Name, UriKind.Relative);

                return ResponseMessage(msg);
            }

            return Created(new Uri("/applications/" + application.Name, UriKind.Relative),
                new
                {});
        }
    }
}