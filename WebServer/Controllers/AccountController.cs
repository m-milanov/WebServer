using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server;
using WebServer.Server.Http;
using WebServer.Server.Results;

namespace WebServer.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) 
            : base(request)
        {
        }

        public ActionResult ActionWithCookies()
        {
            this.Response.AddCookie("Cookie-Name1", "Cookie-Value1");
            this.Response.AddCookie("Cookie-Name2", "Cookie-Value2");

            return Text("Hello");
        }
    }
}
