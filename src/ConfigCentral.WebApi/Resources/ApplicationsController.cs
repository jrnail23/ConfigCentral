using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ConfigCentral.WebApi.Resources
{
    public class ApplicationsController : ApiController
    {
        public static readonly IDictionary<Guid, ApplicationForm> AppsStore =
            new ConcurrentDictionary<Guid, ApplicationForm>();

        [Route("applications")]
        public IHttpActionResult Post([FromBody] ApplicationForm application)
        {
            if (AppsStore.Values.Any(a => a.Name == application.Name))
            {
                var entity = new
                {
                    ErrorDescription = string.Format("An application named '{0}' already exists.", application.Name)
                };
                var msg = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new ObjectContent<object>(entity, new JsonMediaTypeFormatter())
                };
                msg.Headers.Location = new Uri("/applications/" + application.Name, UriKind.Relative);

                return ResponseMessage(msg);
            }

            var appId = Guid.NewGuid();
            AppsStore.Add(appId, application);

            return Created(new Uri("/applications/" + application.Name, UriKind.Relative),
                new
                {});
        }
    }

    public class ApplicationForm
    {
        public string Name { get; set; }
    }
}