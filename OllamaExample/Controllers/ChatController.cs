using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OllamaExample.Models;
using OllamaExample.Services;

namespace OllamaExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        public ChatController(IConversationService conversationService)
        {
            _conversationService = conversationService;

        }
        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return BadRequest("UserId is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Message is required.");
            }
            var result = await _conversationService.SendMessageAsync(request);
            return Ok(result);
        }


    }
}
