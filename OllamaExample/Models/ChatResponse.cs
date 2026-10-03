namespace OllamaExample.Models
{
    public class ChatResponse
    {
        public Guid ConversationId { get; set; }
        public string Response { get; set; } = string.Empty;

    }
}
