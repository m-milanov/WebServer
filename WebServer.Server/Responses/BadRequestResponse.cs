using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http.Response;

namespace WebServer.Server.Responses
{
    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse() 
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
