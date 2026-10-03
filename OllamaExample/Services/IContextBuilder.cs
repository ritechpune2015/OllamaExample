using OllamaExample.Entities;
using OllamaExample.Models;

namespace OllamaExample.Services
{
    public interface IContextBuilder
    {
        List<OllamaMessage> BuildContext( Conversation conversation,
        string currentUserMessage);

    }
}
