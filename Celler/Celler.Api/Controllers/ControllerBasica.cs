using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Celler.Api.Controllers
{
    public class ControllerBasica : ApiController
    {

        public HttpResponseMessage ResponderOk(object dados = null)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { dados });
        }

        public HttpResponseMessage ResponderErro(params string[] mensagens)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { mensagens });
        }

        public HttpResponseMessage ResponderErro(IEnumerable<string> mensagens)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { mensagens });
        }
    }
}
