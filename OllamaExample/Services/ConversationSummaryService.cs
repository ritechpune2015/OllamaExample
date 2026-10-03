using OllamaExample.Entities;
using OllamaExample.Models;

namespace OllamaExample.Services
{
    public class ConversationSummaryService: IConversationSummaryService
    {
        private readonly ILLMService _llmService;

        public ConversationSummaryService( ILLMService llmService)
        {
            _llmService = llmService;
        }

        public async Task<string> GenerateSummaryAsync(List<ConversationMessage> messages)
        {
            var conversationText =
                string.Join(
                    "\n\n",
                    messages.Select(x =>
                        $"{x.Role}: {x.Content}"));

            var summaryPrompt =
                $"""
            Summarize the following conversation.

            Preserve:
            - User's goals
            - Important technical decisions
            - Technologies being used
            - Important project information
            - Unresolved questions

            Remove:
            - Greetings
            - Repetition
            - Unnecessary details

            Write a concise factual summary.

            Conversation:

            {conversationText}
            """;

            var promptMessages =
                new List<OllamaMessage>
                {
                new()
                {
                    Role = "system",
                    Content =
                        "You are a conversation summarization assistant."
                },
                new()
                {
                    Role = "user",
                    Content = summaryPrompt
                }
                };

            return await _llmService.ChatAsync(promptMessages);
        }

    }
}
