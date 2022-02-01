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

        public IRoutingTable Map(string path, HttpMethod method, HttpResponse response)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(response, nameof(response));

            this.routes[method][path] = response;

            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
            => Map(path, HttpMethod.Get, response);

        public IRoutingTable MapPost(string path, HttpResponse response)
            => Map(path, HttpMethod.Post, response);

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            if (!this.routes.ContainsKey(requestMethod) ||
                !this.routes[requestMethod].ContainsKey(requestPath))
                return new NotFoundResponse();

            return this.routes[requestMethod][requestPath];
        
        }
    }
}
