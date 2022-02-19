using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http.Response;

namespace WebServer.Server.Results
{
    public class BadRequestResult : ActionResult
    {
        public BadRequestResult(HttpResponse response) 
            : base(response)
        {
            this.StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
