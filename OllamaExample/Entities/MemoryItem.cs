namespace OllamaExample.Entities
{
    public class MemoryItem
    {
        public Guid MemoryItemId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public int Importance { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
