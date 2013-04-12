using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Requests
{
    public class WebRequester : IRequester
    {
        protected HttpClient client;

        public WebRequester()
        {
            client = new HttpClient(new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                });
            client.DefaultRequestHeaders.Add("User-Agent", "Dalvik/1.6.0 (Linux; U; Android 4.0.4; LT26i Build/6.1.A.2.45)");
            client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("deflate"));
        }

        public void SetBaseUrl(string url)
        {
            client.BaseAddress = new Uri(url);
        }

        public Task<HttpResponseMessage> GetAsync(string relativeUrl)
        {
            return client.GetAsync(relativeUrl);
        }

        public Task<HttpResponseMessage> PostAsync(string relativeUrl, string payload)
        {
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            return client.PostAsync(relativeUrl, content);
        }

        public string Get(string url)
        {
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                throw new RateLimitedException();
            }

            return response.Content.ReadAsStringAsync().Result;
        }
    }

    class RateLimitedException : Exception
    {
        public RateLimitedException()
        {
        }
    }
}
