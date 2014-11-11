using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using ConfigCentral.DomainModel;
using ConfigCentral.Infrastructure;

namespace ConfigCentral.WebApi.Resources.Applications
{
    public class ApplicationsController : ApiController
    {
        private readonly IApplicationRepository _appsStore;
        private readonly Func<IUnitOfWork> _uowFactory;

        public ApplicationsController(IApplicationRepository appsStore, Func<IUnitOfWork> uowFactory)
        {
            _appsStore = appsStore;
            _uowFactory = uowFactory;
        }

        [Route("applications/{name}")]
        public IHttpActionResult Get([FromUri] string name)
        {
            try
            {
                using (_uowFactory())
                {
                    var model = _appsStore.FindByName(name);
                    return Ok(model);
                }
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
                using (_uowFactory())
                {
                    var model = _appsStore.All();
                    return Ok(model);
                }
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("applications")]
        public IHttpActionResult Post([FromBody] ApplicationState application)
        {
            var appEntity = new Application(Guid.NewGuid(), application.Name);

            try
            {
                using (var uow = _uowFactory())
                {
                    _appsStore.Add(appEntity);
                    uow.Commit();
                }
            }
            catch (DuplicateObjectException e)
            {
                // TODO: extract response mapping into an exception filter or something
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

            // TODO: use Hyprlinkr or something like it for strongly-typed routes
            return Created(new Uri("/applications/" + application.Name, UriKind.Relative),
                new
                {});
        }
    }
}