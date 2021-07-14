using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using FidelityHub.Application.Extensions;

namespace FidelityHub.Application.Helpers
{
    public class CustomHttpClient<T> : IDisposable
    {
        private string Url { get; }
        private HttpClient Client { get; }

        public CustomHttpClient(string url)
        {
            this.Url = url;
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri(this.Url);
        }

        public async Task<T> QueryApi(string urlParameters = null)
        {
            // Add an Accept header for JSON format.
            this.Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = this.Client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                return response.Content.ReadAsAsync<T>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                throw new Exception("Error Reading");
            }
        }

        public void Dispose()
        {
            this.Client.Dispose();
        }
    }
}
