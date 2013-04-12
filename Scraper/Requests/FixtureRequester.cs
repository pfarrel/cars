using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Requests
{
    class FixtureRequester : IRequester
    {
        public void SetBaseUrl(string url)
        {
        }

        public Task<HttpResponseMessage> GetAsync(string relativeUrl)
        {
            return Task.FromResult(new HttpResponseMessage());
        }

        public Task<HttpResponseMessage> PostAsync(string relativeUrl, string payload)
        {
            return Task.FromResult(new HttpResponseMessage());
        }

        public string Get(string relativeUrl)
        {
            return "";
        }
    }
}
