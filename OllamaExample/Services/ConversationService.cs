using OllamaExample.Entities;
using OllamaExample.Models;
using OllamaExample.Repositories;

namespace OllamaExample.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _repository;
        private readonly ILLMService _llmService;
        public ConversationService(IConversationRepository repository, ILLMService llmService)
        {
            _repository = repository;
            _llmService = llmService;
        }

        public async Task<ChatResponse> SendMessageAsync(ChatRequest request)
        {
            Conversation conversation;

            // 1. Find or create conversation
            if (request.ConversationId.HasValue)
            {
                conversation =
                    await _repository.GetConversationAsync(
                        request.ConversationId.Value,
                        request.UserId)
                    ?? throw new InvalidOperationException(
                        "Conversation not found.");
            }
            else
            {
                conversation =
                    await _repository.CreateConversationAsync(
                        request.UserId,
                        "New Chat");
            }

            // 2. Add user message
            await _repository.AddMessageAsync(
                conversation.ConversationId,
                "user",
                request.Message);

            // 3. Build LLM messages
            var messages = new List<OllamaMessage> {
            new()
            {
                Role = "system",
                Content =
                    """
                    You are an expert C# and ASP.NET Core teacher.

                    Explain concepts clearly.
                    Use practical examples.
                    Prefer C# examples.
                    """
            }
        };

            foreach (var message in conversation.Messages.OrderBy(x => x.CreatedAt))
            {
                messages.Add(new OllamaMessage
                {
                    Role = message.Role,
                    Content = message.Content
                });
            }

            // Add current user message
            messages.Add(new OllamaMessage
            {
                Role = "user",
                Content = request.Message
            });

            // 4. Call Ollama
            var assistantResponse = await _llmService.ChatAsync(messages);

            // 5. Store assistant response
            await _repository.AddMessageAsync(conversation.ConversationId, "assistant", assistantResponse);

            // 6. Save changes
            await _repository.SaveChangesAsync();

            return new ChatResponse
            {
                ConversationId = conversation.ConversationId,
                Response = assistantResponse
            };
        }
    }
}