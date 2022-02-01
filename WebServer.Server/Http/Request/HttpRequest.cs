using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Server.Http
{
    public class HttpRequest
    {
        private const string NewLine = "\r\n";
        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, string> Query { get; private set; }

        public HttpHeaderCollection Headers { get; private set; } = new HttpHeaderCollection();

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {


            var lines = request.Split(NewLine);

            var startLine = lines.First().Split(" ");

            var method = ParseHttpMethod(startLine[0]);
            var url = startLine[1];

            var (path, query) = ParseUrl(url);


            var headers = ParseHttpHeaders(lines.Skip(1));

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(NewLine, bodyLines);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = headers,
                Body = body
            };

        }

        private static HttpMethod ParseHttpMethod(string method)
            => method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                "" => HttpMethod.Get,
                _ => throw new InvalidOperationException($"Method {method} is not supported.")
            };

        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split("?");

            var path = urlParts[0];
            var query = urlParts.Length > 1
                ? ParseQuery(urlParts[1])
                : new Dictionary<string, string>();

            return (path, query);
        }

        private static Dictionary<string, string> ParseQuery(string queryString)
            => queryString
                .Split("&")
                .Select(part => part.Split("="))
                .Where(part => part.Length == 2)
                .ToDictionary(part => part[0], part => part[1]);



        private static HttpHeaderCollection ParseHttpHeaders(IEnumerable<string> headerLines)
        {
            var headerCollection = new HttpHeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }

                var splitHeadrer = headerLine.Split(": ", 2);

                var headerName = splitHeadrer[0];
                var headerValue = splitHeadrer[1];

                headerCollection.Add(headerName, headerValue);
            }

            return headerCollection;
        }

    }
}
