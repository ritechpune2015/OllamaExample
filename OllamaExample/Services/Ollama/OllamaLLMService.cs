using Microsoft.Extensions.Options;
using OllamaExample.Models;
using OllamaExample.Options;

namespace OllamaExample.Services.Ollama
{
    public class OllamaLLMService : ILLMService
    {
        private readonly HttpClient _httpClient;
        private readonly OllamaOptions _options;

        public OllamaLLMService(HttpClient httpClient,IOptions<OllamaOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<string> ChatAsync(List<OllamaMessage> messages)
        {
            var request = new OllamaChatRequest
            {
                Model = _options.Model,
                Messages = messages,
                Stream = false
            };

            var response = await _httpClient.PostAsJsonAsync(
                "/api/chat",
                request);

            response.EnsureSuccessStatusCode();

            var result =
                await response.Content
                    .ReadFromJsonAsync<OllamaChatResponse>();

            return result?.Message?.Content ?? string.Empty;
        }


        public async Task<string> GenerateAsync(string prompt)
        {
            var request = new OllamaChatRequest
            {
                Model = _options.Model,

                Messages =
                [
                   new OllamaMessage
                   {
                    Role = "user",
                    Content = prompt
                   }
                ],
                Stream = false
            };

            var response = await _httpClient.PostAsJsonAsync("/api/chat",request);

            response.EnsureSuccessStatusCode();

            var result =await response.Content
                    .ReadFromJsonAsync<OllamaChatResponse>();

            return result?.Message?.Content ?? string.Empty;
        }

    }
}
