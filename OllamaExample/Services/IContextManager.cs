using OllamaExample.Entities;
using OllamaExample.Models;

namespace OllamaExample.Services
{
    public interface IContextManager
    {
        Task<List<OllamaMessage>> BuildContextAsync(Conversation conversation, string currentUserMessage);

    }
}
