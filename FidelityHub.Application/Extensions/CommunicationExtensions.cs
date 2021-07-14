using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FidelityHub.Application.Extensions
{
    public static class CommunicationExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this System.Net.Http.HttpContent content)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
        }
    }
}
