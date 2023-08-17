using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = true;
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.ExpectContinue = true;

            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Get Information Status");
            Console.WriteLine("2. Get Success Status");
            Console.WriteLine("3. Get Redirection Status");
            Console.WriteLine("4. Get Client Error Status");
            Console.WriteLine("5. Get Server Error Status");
            Console.WriteLine("6. Get My Name");
            Console.WriteLine("7. Get My Name By Header");
            Console.WriteLine("8. Get My Name By Cookies");
            Console.WriteLine("9. Exit");

            while (true)
            {
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                string url;
                HttpResponseMessage response;

                switch (choice)
                {
                    case 1:
                        url = "http://localhost:8888/Information";
                        response = await client.GetAsync(url);
                        break;
                    case 2:
                        url = "http://localhost:8888/Success";
                        response = await client.GetAsync(url);
                        break;
                    case 3:
                        url = "http://localhost:8888/Redirection";
                        response = await client.GetAsync(url);
                        break;
                    case 4:
                        url = "http://localhost:8888/ClientError";
                        response = await client.GetAsync(url);
                        break;
                    case 5:
                        url = "http://localhost:8888/ServerError";
                        response = await client.GetAsync(url);
                        break;
                    case 6:
                        response = await client.GetAsync("http://localhost:8888/MyName");
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response: " + content);
                        continue;
                    case 7:
                        response = await client.GetAsync("http://localhost:8888/MyNameByHeader");
                        string headerValue = response.Headers.GetValues("X-MyName").FirstOrDefault();
                        Console.WriteLine("X-MyName: " + headerValue);
                        continue;
                    case 8:
                        response = await client.GetAsync("http://localhost:8888/MyNameByCookies");
                        if (response.IsSuccessStatusCode)
                        {
                            foreach (Cookie cookie in handler.CookieContainer.GetCookies(new Uri("http://localhost:8888")))
                            {
                                if (cookie.Name == "MyName")
                                {
                                    Console.WriteLine("MyName Cookie: " + cookie.Value);
                                }
                            }
                        }
                        continue;
                    case 9:
                        try
                        {
                            await client.GetAsync("http://localhost:8888/exit");
                        }
                        catch (System.Net.Http.HttpRequestException ex)
                        {
                            Console.WriteLine("Exiting...");
                        }
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue;
                }

                Console.WriteLine("Response Status Code: " + response.StatusCode);
            }

        }
    }
}