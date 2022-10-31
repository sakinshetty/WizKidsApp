using Microsoft.Net.Http.Headers;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text.Json;

namespace WizKidAPI.Client
{
    public class HttpClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;

        public HttpClientFactory(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
               

        public async Task<string[]>  OnGet(string locale, string text)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get, "https://services.lingapps.dk/misc/getPredictions?locale=" + locale + "&text=" + text)
                {
                    Headers =
                    {
                        { HeaderNames.Accept, "application/json" }
                    }
                };           
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.GetSection("BearerToken").GetSection("Token").Value);
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                var getPredictions = await JsonSerializer.DeserializeAsync
                    <string[]>(contentStream);
                return getPredictions;
            }
            return null;
        }
    }
}
