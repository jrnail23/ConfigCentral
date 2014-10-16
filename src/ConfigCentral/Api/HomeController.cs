using System.Web.Http;

namespace ConfigCentral.Api
{
    [RoutePrefix("api")]
    public class HomeController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}