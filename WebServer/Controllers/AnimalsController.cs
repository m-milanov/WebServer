using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.Animals;
using WebServer.Server;
using WebServer.Server.Controllers;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;
using WebServer.Server.Results;

namespace WebServer.Controllers
{
    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request)
            :base(request)
        {
        }
        

        public HttpResponse Cats()
        {

            var query = this.Request.Query;
            var catName = query.ContainsKey("Name")
                ? query["Name"]
                : "Paco";
            
            var catAge = query.ContainsKey("Age")
                ? int.Parse(query["Age"])
                : 0;

            var viewModel = new CatViewModel 
            { 
                Name = catName,
                Age = catAge
            };



            return View(viewModel); 
        }

        public HttpResponse Dogs()
            => View();

        public HttpResponse Bunnies()
            => View("Rabbits");

        public HttpResponse Turtles()
            => View("/Animals/Wild/Turtles");




    }
}
