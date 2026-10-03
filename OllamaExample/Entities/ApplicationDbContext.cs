using Microsoft.EntityFrameworkCore;

namespace OllamaExample.Entities
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(
     DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Conversation> Conversations => Set<Conversation>();

        public DbSet<ConversationMessage> ConversationMessages  => Set<ConversationMessage>();

        }
    }
