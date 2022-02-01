using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server;
using WebServer.Server.Responses;

namespace WebServer
{
    class Startup
    { 
        static async Task Main()
            => await new HttpServer(routes => routes
            .MapGet("/", new TextResponse("this is text response from home"))
            .MapGet("/Cats", new HtmlResponse("<h1>this is html response from the cats<h1>")))
            .Start();

            
    }
}
