using System.Web.Http;

namespace ConfigCentral.WebApi
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