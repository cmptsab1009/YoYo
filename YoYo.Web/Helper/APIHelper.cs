using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YoYo.Web.Helper
{
    public static class APIHelper
    {
        public static string GetHttpContent(string uri, CookieContainer container, HttpMethod methodType, string bodyParameters = null)
        {
            using HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                CookieContainer = container,
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18363");
            client.Timeout = TimeSpan.FromMinutes(3.0);
            using HttpRequestMessage request = new HttpRequestMessage
            {
                Method = methodType,
                RequestUri = new Uri(uri)
               
            };
            if (!string.IsNullOrEmpty(bodyParameters))
                request.Content = new StringContent(bodyParameters, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = client.SendAsync(request).GetAwaiter().GetResult();
            using HttpContent content = response.Content;
            string apiResponse = content.ReadAsStringAsync().GetAwaiter().GetResult();
            return apiResponse;
        }
    }
}
