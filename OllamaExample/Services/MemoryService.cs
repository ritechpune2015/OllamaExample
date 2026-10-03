using OllamaExample.Entities;

namespace OllamaExample.Services
{
    public class MemoryService : IMemoryService
    {
        private readonly IMemoryStore _memoryStore;

        public MemoryService(IMemoryStore memoryStore)
        {
            _memoryStore = memoryStore;
        }

        public async Task RememberAsync(string userId, string content, string type, int importance)
        {
            if (string.IsNullOrWhiteSpace(content))
                return;

            var memory = new MemoryItem
            {
                MemoryItemId = Guid.NewGuid(),
                UserId = userId,
                Content = content.Trim(),
                Type = type,
                Importance = importance,
                CreatedAt = DateTime.UtcNow
            };

            await _memoryStore.AddAsync(memory);
        }

        public async Task<List<MemoryItem>> RecallAsync(string userId)
        {
            return await _memoryStore
                .GetAllAsync(userId);
        }

        public async Task<List<MemoryItem>> SearchAsync(string userId, string query)
        {
            var memories =
                await _memoryStore.GetAllAsync(userId);

            if (string.IsNullOrWhiteSpace(query))
                return memories;

            var words = query
                .ToLowerInvariant()
                .Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries);

            return memories
                .Where(memory =>
                    words.Any(word =>
                        memory.Content
                            .Contains(
                                word,
                                StringComparison
                                    .OrdinalIgnoreCase)))
                .OrderByDescending(x => x.Importance)
                .ToList();
        }

        public async Task ForgetAsync(
            string userId,
            Guid memoryId)
        {
            await _memoryStore.DeleteAsync(
                userId,
                memoryId);
        }
    }

}
