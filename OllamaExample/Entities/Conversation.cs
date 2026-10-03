namespace OllamaExample.Entities
{
    public class Conversation
    {
        public Guid ConversationId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? SummaryUpdatedAt { get; set; }

        public ICollection<ConversationMessage> Messages { get; set; }
            = new List<ConversationMessage>();

    }

}
