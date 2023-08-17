using Listener;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Listener
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                Console.WriteLine($"Received a request: {request.HttpMethod} {request.Url}"); // Log received request

                string resourcePath = ParseRequest(request);

                if (resourcePath.ToLower() == "information")
                {
                    await HttpResponses.SendInformationResponseAsync(response);
                }
                else if (resourcePath.ToLower() == "success")
                {
                    HttpResponses.SendSuccessResponse(response);
                }
                else if (resourcePath.ToLower() == "redirection")
                {
                    HttpResponses.SendRedirectionResponse(response);
                }
                else if (resourcePath.ToLower() == "clienterror")
                {
                    HttpResponses.SendClientErrorResponse(response);
                }
                else if (resourcePath.ToLower() == "servererror")
                {
                    HttpResponses.SendServerErrorResponse(response);
                }
                else if (resourcePath.ToLower() == "myname")
                {
                    string responseStr = HttpResponses.GetMyName();
                    byte[] buffer = Encoding.UTF8.GetBytes(responseStr);
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                else if (resourcePath.ToLower() == "mynamebyheader")
                {
                    HttpResponses.SetMyNameByHeader(response);
                }
                else if (resourcePath.ToLower() == "mynamebycookies")
                {
                    HttpResponses.SetMyNameByCookies(response);
                }
                else if (resourcePath.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                response.OutputStream.Close();
            }

            listener.Stop();
        }

        static string ParseRequest(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.Substring(1);
        }
    }
}