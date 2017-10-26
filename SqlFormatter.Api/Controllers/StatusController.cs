using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SqlFormatter.Api.Controllers
{
    [RoutePrefix("api/status")]
    public class StatusController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
