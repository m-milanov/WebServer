using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;

namespace WebServer.Controllers
{
    public class CatsController : Controller
    {
        public CatsController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Create()
            => View();

        public HttpResponse Save()
        {
            var name = this.Request.Form["Name"];
            var age = this.Request.Form["Age"];


            return Text($"{name} - {age}");
        }
    }
}
