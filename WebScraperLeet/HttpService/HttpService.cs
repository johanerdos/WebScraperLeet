using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraperLeet.HttpService
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> FetchUrlContentAsync(string path)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{path}");

            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Could not fetch content from url: {_httpClient.BaseAddress}/{path}");
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
