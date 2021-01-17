using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmployeeManager.APITest
{
    //https://basquang.wordpress.com/2017/04/27/hello-asp-net-web-api/
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            await GetSampleAsync();
            Console.ReadLine();
        }
        static async Task GetSampleAsync()
        {
            // this is where we will send it
            string uri = "http://localhost:3649/api/values";

            // create a request
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(uri); request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "GET";

            /*====HTTP POST====*/
            //request.Method = "POST";
            //// turn our request string into a byte stream
            //byte[] postBytes = Encoding.ASCII.GetBytes("hello"); //param

            //// this is important - make sure you specify type this way
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = postBytes.Length;
            //Stream requestStream = request.GetRequestStream();

            //// now send it
            //requestStream.Write(postBytes, 0, postBytes.Length);
            //requestStream.Close();

            // grab te response and print it out to the console along with the status code
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
            Console.WriteLine(response.StatusCode);
        }

        

    }
}
