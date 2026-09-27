using OllamaExample.Models;

namespace OllamaExample.Services
{
    public class ConversationService : IConversationService
    {
        private readonly ILLMService _llmService;

        private readonly List<OllamaMessage> _messages = [];

        public ConversationService(ILLMService llmService)
        {
            _llmService = llmService;

            _messages.Add(new OllamaMessage
            {
                Role = "system",
                Content =
                    """
                You are an expert C# and ASP.NET Core teacher.

                Explain concepts clearly.
                Use practical examples.
                Assume the developer has 1-3 years of experience.
                Prefer C# and ASP.NET Core examples.
                """
            });
        }

        public async Task<string> SendMessageAsync(string userMessage)
        {
            // 1. Add user message
            _messages.Add(new OllamaMessage
            {
                Role = "user",
                Content = userMessage
            });

            // 2. Send complete conversation to LLM
            var response =
                await _llmService.ChatAsync(_messages);

            // 3. Store assistant response
            _messages.Add(new OllamaMessage
            {
                Role = "assistant",
                Content = response
            });

            return response;
        }

    }
}
