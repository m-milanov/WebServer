using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;
using WebServer.Server.Responses;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request)
            :base(request)
        {
        }
        public HttpResponse Index()
             => Text("This is text response from the home.");

        public HttpResponse SoftUni()
            => Redirect("http://softuni.bg");
       
    }
}
