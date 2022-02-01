using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Common;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;
using WebServer.Server.Responses;

namespace WebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;

        public RoutingTable()
        {
            this.routes = new()
            {
                [HttpMethod.Get] = new(),
                [HttpMethod.Post] = new(),
                [HttpMethod.Put] = new(),
                [HttpMethod.Delete] = new(),

            };
        }

        public IRoutingTable Map(string url, HttpMethod method, HttpResponse response)
        {
            return method switch
            {
                HttpMethod.Get => this.MapGet(url, response),
                _ => throw new System.Exception(),
            };
        }

        public IRoutingTable MapGet(string url, HttpResponse response)
        {
            Guard.AgainstNull(url);

            this.routes[HttpMethod.Get][url] = response;

            return this;
        }

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Url;

            if (!this.routes.ContainsKey(requestMethod) ||
                !this.routes[requestMethod].ContainsKey(requestUrl))
                return new NotFoundResponse();

            return this.routes[requestMethod][requestUrl];
        
        }
    }
}
