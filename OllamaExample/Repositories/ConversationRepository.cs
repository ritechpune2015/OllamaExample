using Microsoft.EntityFrameworkCore;
using OllamaExample.Entities;

namespace OllamaExample.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;

        public ConversationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation?> GetConversationAsync(  Guid conversationId,
            string userId)
        {
            return await _context.Conversations
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(
                    x =>
                        x.ConversationId == conversationId &&
                        x.UserId == userId);
        }

        public async Task<Conversation> CreateConversationAsync(string userId, string title)
        {
            var conversation = new Conversation
            {
                ConversationId = Guid.NewGuid(),
                UserId = userId,
                Title = title,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Conversations.Add(conversation);

            await _context.SaveChangesAsync();

            return conversation;
        }

        public async Task AddMessageAsync(Guid conversationId, string role,string content)
        {
            var message = new ConversationMessage
            {
                ConversationId = conversationId,
                Role = role,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            _context.ConversationMessages.Add(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
