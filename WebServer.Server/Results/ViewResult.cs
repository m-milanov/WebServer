using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;

namespace WebServer.Server.Results
{
    public class ViewResult : ActionResult
    {
        private const char PATHSEPARATOR = '/';
        public ViewResult(
            HttpResponse response,
            string viewName, 
            string controllerName, 
            object model) 
            : base(response)
        {
            this.StatusCode = HttpStatusCode.OK;
            this.GetHtml(viewName, controllerName, model);
        }

        private void GetHtml(string viewName, string controllerName, object model)
        {


            if (!viewName.Contains(PATHSEPARATOR))
            {
                viewName = controllerName + PATHSEPARATOR + viewName;
            }

            var viewPath = Path.GetFullPath("./Views/" + viewName.TrimStart(PATHSEPARATOR) + ".cshtml");

            if (!File.Exists(viewPath))
            {
                this.ExecuteMissingViewError(viewPath);
                return;
            }

            var viewContent = File.ReadAllText(viewPath);

            if(model != null)
            {
                viewContent = PopulateModel(viewContent, model);
            }

            this.PrepareContent(viewContent, HttpContentType.Html);
        }

        private void ExecuteMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;

            string errorMessage = $"View '{viewPath}' was not found.";

            this.PrepareContent(errorMessage, HttpContentType.PlainText);
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(
                pr => new
                {
                    Name = pr.Name,
                    Value = pr.GetValue(model)
                });

            foreach(var entry in data)
            {
                viewContent = viewContent.Replace($"{{{{{entry.Name}}}}}", entry.Value.ToString());
            }

            return viewContent;
        }
    }
}
