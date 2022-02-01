using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Common;
using WebServer.Server.Http.Response;

namespace WebServer.Server.Responses
{
    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", contentLength);

            this.Content = text;
        }

    }
}
