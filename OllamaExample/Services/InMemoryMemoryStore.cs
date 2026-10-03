using OllamaExample.Entities;

namespace OllamaExample.Services
{
    public class InMemoryMemoryStore : IMemoryStore
    {
        private readonly List<MemoryItem> _memories = [];

        public Task AddAsync(MemoryItem memory)
        {
            _memories.Add(memory);

            return Task.CompletedTask;
        }

        public Task<List<MemoryItem>> GetAllAsync(string userId)
        {
            var result = _memories
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Importance)
                .ThenByDescending(x => x.CreatedAt)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<MemoryItem?> GetByIdAsync(string userId, Guid memoryId)
        {
            var memory = _memories.FirstOrDefault(x => x.MemoryItemId == memoryId &&
                     x.UserId == userId);

            return Task.FromResult(memory);
        }

        public Task DeleteAsync(string userId, Guid memoryId)
        {
            var memory = _memories.FirstOrDefault(x => x.MemoryItemId == memoryId &&
                     x.UserId == userId);

            if (memory != null)
            {
                _memories.Remove(memory);
            }

            return Task.CompletedTask;
        }
    }

}
