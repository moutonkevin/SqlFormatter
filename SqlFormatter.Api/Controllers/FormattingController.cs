using System.Web.Http;
using Autofac.Features.Indexed;
using SqlFormatter.Core.Interfaces;

namespace SqlFormatter.Api.Controllers
{
    [RoutePrefix("api/formatting")]
    public class FormattingController : ApiController
    {
        private readonly IFormattor _formattor;

        public FormattingController(IIndex<string, IFormattor> formattor)
        {
            _formattor = formattor["SqlFormattorProcessor"];
        }

        [HttpPost]
        [Route("sql")]
        public string SqlFormatter([FromBody]string value)
        {
            return _formattor.Format(value);
        }
    }
}
