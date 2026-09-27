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
        private readonly ILLMService _llmservice;
        public ChatController(ILLMService _llmservice)
        {
            this._llmservice = _llmservice;
        }
    
        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            var result = await this._llmservice.GenerateAsync(request.Message);
            return Ok(result);
        }

    }
}
