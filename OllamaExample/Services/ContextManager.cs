using OllamaExample.Entities;
using OllamaExample.Models;

namespace OllamaExample.Services
{
    public class ContextManager : IContextManager
    {
        private const int RecentMessageCount = 10;

        private readonly IConversationSummaryService  _summaryService;

        public ContextManager(IConversationSummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<List<OllamaMessage>>
            BuildContextAsync(
                Conversation conversation,
                string currentUserMessage)
        {
            var messages =
                conversation.Messages
                    .OrderBy(x => x.CreatedAt)
                    .ToList();

            var recentMessages =
                messages
                    .TakeLast(RecentMessageCount)
                    .ToList();

            var result = new List<OllamaMessage>();

            result.Add(new OllamaMessage
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

            if (!string.IsNullOrWhiteSpace(
                conversation.Summary))
            {
                result.Add(new OllamaMessage
                {
                    Role = "system",
                    Content =
                        $"Conversation summary:\n" +
                        conversation.Summary
                });
            }

            foreach (var message in recentMessages)
            {
                result.Add(new OllamaMessage
                {
                    Role = message.Role,
                    Content = message.Content
                });
            }

            result.Add(new OllamaMessage
            {
                Role = "user",
                Content = currentUserMessage
            });

            return result;
        }
    }

}
