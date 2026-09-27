using Microsoft.Extensions.Options;
using OllamaExample.Options;
using OllamaExample.Services;
using OllamaExample.Services.Ollama;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ILLMService, OllamaLLMService>(
    (serviceProvider, client) =>
    {
        var options =serviceProvider.GetRequiredService<IOptions<OllamaOptions>>().Value;
        client.BaseAddress =
            new Uri(options.BaseUrl);
        client.Timeout = TimeSpan.FromMinutes(10);
    });

builder.Services.Configure<OllamaOptions>(builder.Configuration.GetSection("Ollama"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//builder.Services.AddScoped<IConversationService,ConversationService>();
builder.Services.AddSingleton<IConversationService, ConversationService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
