using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Listener
{
    public class HttpResponses
    {
        /// <summary>
        /// HttpClient is designed to handle the 100 Continue status internally as an 
        /// intermediate status used for specific cases where the client expects the server to check 
        /// the request headers before sending the body content by expecting the server to send a final 
        /// status code, so it's not possible to return the 1XX code to the client.
        /// So I implemented a kinda simulation of the use of this code
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task SendInformationResponseAsync(HttpListenerResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.Continue;
            response.StatusDescription = "Continue";
            response.SendChunked = true;
            response.OutputStream.Flush();
            await Task.Delay(500);
            Console.WriteLine("Processing request, Status Code:" + response.StatusCode.ToString());

            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusDescription = "OK";
            byte[] buffer = Encoding.UTF8.GetBytes("Success");
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        public static void SendSuccessResponse(HttpListenerResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusDescription = "OK";
        }

        public static void SendRedirectionResponse(HttpListenerResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.Moved;
            response.StatusDescription = "Moved Permanently";
        }

        public static void SendClientErrorResponse(HttpListenerResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.StatusDescription = "Bad Request";
        }

        public static void SendServerErrorResponse(HttpListenerResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusDescription = "Internal Server Error";
        }

        public static void SetMyNameByHeader(HttpListenerResponse response)
        {
            response.Headers.Add("X-MyName", "Jose Roman");
            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusDescription = "OK";
        }

        public static void SetMyNameByCookies(HttpListenerResponse response)
        {
            Cookie myNameCookie = new Cookie("MyName", "Jose Roman");
            response.AppendCookie(myNameCookie);
            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusDescription = "OK";
        }

        public static string GetMyName()
        {
            return "Jose Roman";
        }
    }
}
