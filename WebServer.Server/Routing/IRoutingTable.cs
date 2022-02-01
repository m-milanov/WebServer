using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;

namespace WebServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(string path, HttpMethod method, HttpResponse response);

        IRoutingTable Map(string path, HttpMethod method, Func<HttpRequest, HttpResponse> responseFunction);

        IRoutingTable MapGet(string path, HttpResponse response);

        IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction);

        
        IRoutingTable MapPost(string path, HttpResponse response);

        IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction);

        
    }
}
