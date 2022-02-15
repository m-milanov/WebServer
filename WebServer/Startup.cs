using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebServer.Controllers;
using WebServer.Server;
using WebServer.Server.Controllers;
using WebServer.Server.Responses;

namespace WebServer
{
    public class Startup
    { 
        static async Task Main()
            => await new HttpServer(routes => routes
                .MapGet<HomeController>("/", c => c.Index())
                .MapGet<HomeController>("/Softuni", c => c.SoftUni())
                .MapGet<AnimalsController>("/Cats", c => c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
                .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
                .MapGet<CatsController>("/Cats/Create", c => c.Create())
                .MapPost<CatsController>("/Cats/Save", c => c.Save()))
            .Start();

    }
}
