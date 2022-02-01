using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http;
using WebServer.Server.Http.Response;
using WebServer.Server.Routing;

namespace WebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        private readonly RoutingTable routingTable;

        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfig)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            listener = new TcpListener(this.ipAddress, port);

            this.routingTable = new RoutingTable();
            routingTableConfig(this.routingTable);   
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable)
            :this("127.0.0.1", port, routingTable)
        {
        }

        public HttpServer(Action<IRoutingTable> routingTable)
            :this("127.0.0.1", 5000, routingTable)
        {
        }

        public async Task Start()
        {

            this.listener.Start();

            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Listening for request");

            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = await this.ReadRequest(networkStream);

                if (string.IsNullOrEmpty(requestText))
                    continue;

                Console.WriteLine(requestText);

                var request = HttpRequest.Parse(requestText);

                var response = this.routingTable.MatchRequest(request);

                await this.WriteResponse(networkStream, response);

                connection.Close();
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var totalBytesRead = 0;

            var requestBuilder = new StringBuilder();

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
                  
                totalBytesRead += bytesRead;

                if (totalBytesRead > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large.");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }

        private async Task WriteResponse(
            NetworkStream networkStream, 
            HttpResponse response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responseBytes);
        }
    }
}
