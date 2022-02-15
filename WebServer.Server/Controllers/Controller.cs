using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;
using WebServer.Server.Responses;

namespace WebServer.Server
{
    public abstract class Controller
    {
        protected Controller(HttpRequest request)
            => this.Request = request;
        protected HttpRequest Request { get; private init; }

        protected HttpResponse Text(string text)
            => new TextResponse(text);

        protected HttpResponse Html(string html)
            => new HtmlResponse(html);

        protected HttpResponse Redirect(string location)
            => new RedirectResponse(location);

        protected HttpResponse View([CallerMemberName] string viewName = "")
            => new ViewResponse(viewName, GetControllerName(), null);

        protected HttpResponse View(string viewName, object model)
            => new ViewResponse(viewName, GetControllerName(), model);

        protected HttpResponse View(object model, [CallerMemberName] string viewName = "")
            => new ViewResponse(viewName, GetControllerName(), model);

        private string GetControllerName()
            => this.GetType().Name.
                        Replace(nameof(Controller), string.Empty);
    }
}
