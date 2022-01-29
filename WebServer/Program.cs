using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server;

namespace WebServer
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var server = new HttpServer("127.0.0.1", 8080);

            await server.Start();
            
        }
    }
}
