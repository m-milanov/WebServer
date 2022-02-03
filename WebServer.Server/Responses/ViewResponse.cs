using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;

namespace WebServer.Server.Responses
{
    public class ViewResponse : HttpResponse
    {
        private const char PATHSEPARATOR = '/';
        public ViewResponse(string viewName, string controllerName) 
            : base(HttpStatusCode.OK)
        {
            this.GetHtml(viewName, controllerName);
        }

        private void GetHtml(string viewName, string controllerName)
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

            this.PrepareContent(viewContent, HttpContentType.Html);
        }

        private void ExecuteMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;

            string errorMessage = $"View '{viewPath}' was not found.";

            this.PrepareContent(errorMessage, HttpContentType.PlainText);
        }

    }
}
