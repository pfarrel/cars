using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Scraper.Requests;

namespace Scraper
{
    public abstract class ApiBase
    {
        protected IRequester Requester { get; set; }

        public ApiBase(IRequester webRequester, string baseUrl)
        {
            Requester = webRequester;
            Requester.SetBaseUrl(baseUrl);
        }
    }
}
