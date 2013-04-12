using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Requests
{
    public interface IRequester
    {
        void SetBaseUrl(string url);
        Task<HttpResponseMessage> GetAsync(string relativeUrl);
        Task<HttpResponseMessage> PostAsync(string relativeUrl, string payload);
        string Get(string relativeUrl);
    }
}
