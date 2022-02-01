using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server;
using WebServer.Server.Controllers;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;
using WebServer.Server.Responses;

namespace WebServer.Controllers
{
    public class AnimalController : Controller
    {
        public AnimalController(HttpRequest request)
            :base(request)
        {
        }
        

        public HttpResponse Cats()
        {
            var query = this.Request.Query;
            var catName = query.ContainsKey("Name")
                ? query["Name"]
                : "the cats";

            var result = $"<h1>this is html response from {catName}<h1>";

            return Html(result);
        }

        public HttpResponse Dogs()
        {
            return Html("<h2> this is html response from dogs</h2>");
        }

    }
}
