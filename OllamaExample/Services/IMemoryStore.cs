using OllamaExample.Entities;

namespace OllamaExample.Services
{
    public interface IMemoryStore
    {
        Task AddAsync(MemoryItem memory);

        Task<List<MemoryItem>> GetAllAsync(string userId);

        Task<MemoryItem?> GetByIdAsync(string userId, Guid memoryId);

        Task DeleteAsync(string userId,Guid memoryId);
    }

}
