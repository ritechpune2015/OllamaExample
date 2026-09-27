using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OllamaExample.Models;
using OllamaExample.Services;

namespace OllamaExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationServiceController : ControllerBase
    {
        private readonly IConversationService _conservice;
        public ConversationServiceController(IConversationService conservice)
        {
            this._conservice = conservice;
        }

        [HttpPost]
        public async Task<IActionResult> Chat(
        [FromBody] ChatRequest request)
        {
            var response =
                await _conservice.SendMessageAsync(request.Message);

            return Ok(new
            {
                response
            });
        }

    }
}
