using OllamaExample.Entities;
using OllamaExample.Models;

namespace OllamaExample.Services
{
    public class ContextBuilder : IContextBuilder
    {
        private const int RecentMessageCount = 10;

        public List<OllamaMessage> BuildContext(Conversation conversation,
            string currentUserMessage)
        {
            var messages = new List<OllamaMessage>();

            // 1. System prompt
            messages.Add(new OllamaMessage
            {
                Role = "system",
                Content =
                    """
                You are an expert C# and ASP.NET Core teacher.

                Explain concepts clearly.
                Use practical examples.
                Prefer C# examples.
                """
            });

            // 2. Conversation summary
            if (!string.IsNullOrWhiteSpace(conversation.Summary))
            {
                messages.Add(new OllamaMessage
                {
                    Role = "system",
                    Content =
                        $"Conversation summary:\n{conversation.Summary}"
                });
            }

            // 3. Recent messages
            var recentMessages =
                conversation.Messages
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(RecentMessageCount)
                    .OrderBy(x => x.CreatedAt);

            foreach (var message in recentMessages)
            {
                messages.Add(new OllamaMessage
                {
                    Role = message.Role,
                    Content = message.Content
                });
            }

            // 4. Current message
            messages.Add(new OllamaMessage
            {
                Role = "user",
                Content = currentUserMessage
            });

            return messages;
        }
    }

}
