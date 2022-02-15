using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;

namespace WebServer.Server.Responses
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string location)
            : base(HttpStatusCode.Found)
            => this.Headers.Add(HttpHeader.Location, new HttpHeader(HttpHeader.Location, location));
    }
}
