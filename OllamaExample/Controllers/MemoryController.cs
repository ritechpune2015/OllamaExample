using Microsoft.AspNetCore.Mvc;
using OllamaExample.Services;

namespace OllamaExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoryController : ControllerBase
    {
        private readonly IMemoryService _memoryService;

        public MemoryController(IMemoryService memoryService)
        {
            _memoryService = memoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Remember(string userId, string content,string type,
            int importance = 5)
        {
            await _memoryService.RememberAsync(userId,content,type,  importance);

            return Ok(new
            {
                message = "Memory stored successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> Recall(string userId)
        {
            var memories = await _memoryService.RecallAsync(userId);

            return Ok(memories);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string userId,string query)
        {
            var memories =
                await _memoryService.SearchAsync(
                    userId,
                    query);

            return Ok(memories);
        }

        [HttpDelete("{memoryId:guid}")]
        public async Task<IActionResult> Forget(string userId,Guid memoryId)
        {
            await _memoryService.ForgetAsync(
                userId,
                memoryId);

            return NoContent();
        }
    }

}
