using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



var builder = Host.CreateApplicationBuilder();

builder.Services.AddChatClient(new OllamaChatClient(
    endpoint: new Uri("http://localhost:11434"),
    modelId: "llama3.2")
).Build();

var app = builder.Build();
var chatClient = app.Services.GetRequiredService<IChatClient>();

var chatCompletion = await chatClient.CompleteAsync("How can you help me?");
Console.WriteLine(chatCompletion.Message.Text);


Console.ReadKey();