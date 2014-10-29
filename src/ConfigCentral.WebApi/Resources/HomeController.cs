using System.Web.Http;

namespace ConfigCentral.WebApi.Resources
{
    public class HomeController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}