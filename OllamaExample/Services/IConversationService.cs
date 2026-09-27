namespace OllamaExample.Services
{
    public interface IConversationService
    {
        Task<string> SendMessageAsync(
            string userMessage);
    }
}
