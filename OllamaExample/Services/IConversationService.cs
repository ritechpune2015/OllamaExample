using OllamaExample.Models;

namespace OllamaExample.Services
{
    public interface IConversationService
    {
        Task<ChatResponse> SendMessageAsync(ChatRequest request);
    }
}
