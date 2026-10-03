using OllamaExample.Entities;

namespace OllamaExample.Services
{
    public interface IConversationSummaryService
    {
        Task<string> GenerateSummaryAsync(List<ConversationMessage> messages);
    }
}
