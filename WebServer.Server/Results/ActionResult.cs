using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;

namespace WebServer.Server.Results
{
    public class ActionResult : HttpResponse
    {
        public ActionResult(HttpResponse response) 
            : base(response.StatusCode)
        {
            //this.Response = response;

            this.PrepareHeaders(response.Headers);

            this.PrepareCookies(response.Cookies);

            this.Content = response.Content;
        }

        //protected HttpResponse Response { get; init; }

        private void PrepareHeaders(IDictionary<string, HttpHeader> headers)
        {
            foreach (var header in headers.Values)
                this.AddHeader(header.Name, header.Value);
        }

        private void PrepareCookies(IDictionary<string, HttpCookie> cookies)
        {
            foreach (var cookie in cookies.Values)
                this.AddCookie(cookie.Name, cookie.Value);
        }
    }
}
