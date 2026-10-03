namespace OllamaExample.Models
{
    public class ChatRequest
    {
        public string UserId { get; set; } = string.Empty;
        public Guid? ConversationId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
