using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    public abstract class ApiBase
    {
        protected HttpClient client;

        protected ApiBase(string baseUrl)
        {
            client = new HttpClient(new HttpClientHandler()
                { 
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                });
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "Dalvik/1.6.0 (Linux; U; Android 4.0.4; LT26i Build/6.1.A.2.45)");
            client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("deflate"));
        }

        protected string MakeRequestSynchronous(string url)
        {
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                throw new ApplicationException("Request failed - rate limited or blocked (204 No Content)");
            }

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
