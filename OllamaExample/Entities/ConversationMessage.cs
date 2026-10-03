namespace OllamaExample.Entities
{
    public class ConversationMessage
    {
        public long ConversationMessageId { get; set; }
        public Guid ConversationId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Conversation Conversation { get; set; } = null!;

    }
}
