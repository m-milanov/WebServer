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
        IRoutingTable MapGet(string url, HttpResponse response);
        IRoutingTable Map(string url, HttpMethod method, HttpResponse response);


    }
}
