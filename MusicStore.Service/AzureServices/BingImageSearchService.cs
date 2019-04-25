using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.AzureServices
{
    public class BingImageSearchService
    {
        const string subscriptionKey = "";
        const string uriBase = "https://centralus.api.cognitive.microsoft.com/bing/v7.0/images/search";

        public BingImageSearchService() {
            //some DI here
        }

        public async Task<SearchResultModel> Search(string keyword) {
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(keyword);
            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
            var response = await request.GetResponseAsync();
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var searchResult = new SearchResultModel()
            {
                jsonResult = json,
                relevantHeaders = new Dictionary<string, string>()
            };

            // Extract Bing HTTP headers
            foreach (string header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.relevantHeaders[header] = response.Headers[header];
            }
            return searchResult;
        }


    }
}
