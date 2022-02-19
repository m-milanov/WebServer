using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Common;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;
using WebServer.Server.Results;

namespace WebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest, HttpResponse>>> routes;

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
            Guard.AgainstNull(response, nameof(response));

            return this.Map(path, method, request => response);
        }

        public IRoutingTable Map(string path, HttpMethod method, Func<HttpRequest, HttpResponse> responseFunction)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunction, nameof(responseFunction));

            this.routes[method][path.ToLower()] = responseFunction;

            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
            => Map(path, HttpMethod.Get, response);
        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction)
            => Map(path, HttpMethod.Get, responseFunction);

        public IRoutingTable MapPost(string path, HttpResponse response)
            => Map(path, HttpMethod.Post, response);
        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction)
            => Map(path, HttpMethod.Post, responseFunction);

        public HttpResponse ExecuteRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            if (!this.routes.ContainsKey(requestMethod) ||
                !this.routes[requestMethod].ContainsKey(requestPath))
                return new HttpResponse(HttpStatusCode.NotFound);

            var responseFunction = this.routes[requestMethod][requestPath];

            return responseFunction(request);
        }

       
    }
}
