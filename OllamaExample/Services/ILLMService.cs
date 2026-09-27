using OllamaExample.Models;

namespace OllamaExample.Services
{
    public interface ILLMService
    {
        Task<string> GenerateAsync(string prompt);
        Task<string> ChatAsync(List<OllamaMessage> messages);

    }
}
