using System.Text.Json;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

////////   1.) BUILD THE DEPENDENCY INJECTION   //////// 
var builder = Host.CreateApplicationBuilder();
var chatClient = builder.Services.AddChatClient(new OllamaChatClient(endpoint: new Uri("http://localhost:11434"), modelId: "llama3.2-vision")).Build();


////////   2.) CREATE SYSTEM PROMPT   ////////
var systemPrompt = new ChatMessage
{
    Role = ChatRole.System,
    Text = @"
               You are an AI model that must strictly respond in a well-formed JSON format.
               You analyze images for the social platform Instagram for aesthetics on a scale from 1 to 10.
               Consider composition, lighting, and engagement potential.
               After analyzing an image, **always** return a valid, fully enclosed JSON object.
               Return always the result as strictly in JSON format in the following structure:
               {
                 'rating': <number between 1 and 10>,
                 'reason': '<brief explanation of why the rating was given>'
               }

               **Use the filename exactly as provided in the user prompt**—do not change or generate one.
               Do not wrap with JSON ``` wrappers.
               Return JSON as pretty formatted.
               If the response is not a valid JSON object, **correct yourself and return a valid JSON format immediately.**
               "
};


////////   3.) ASK TO AI   ////////
var images = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\photos")).GetFiles("*.jpg");
var aiResponses = new List<AiResponse>(images.Length);
foreach (var image in images)
{
    var prompt = "Analyze this image: " + image.Name;

    var messages = new List<ChatMessage>
    {
        systemPrompt,
        new ChatMessage
        {
            Role = ChatRole.User,
            Text = prompt,
            Contents = new List<AIContent> { new ImageContent(File.ReadAllBytes(image.FullName)) }
        }
    };

    Console.WriteLine("» PROMPT: " + prompt);
    var chatResponse = await chatClient.CompleteAsync(messages);

    Console.WriteLine("» AI-RESPONSE: \n" + chatResponse.Message.Text + "\n" + new string('═', 100));
    var aiResponse = JsonSerializer.Deserialize<AiResponse>(chatResponse.Message.Text);
    aiResponse.Filename = image.Name;
    aiResponses.Add(aiResponse);
    //messages.Add(chatResponse.Message);
}


//////   4.) FIND THE WINNER   ////////
var winner = aiResponses.OrderByDescending(x => x.Rating).FirstOrDefault();
Console.WriteLine("» WINNER: " + winner?.Filename);


////////   5.) DISPOSE   ////////
Console.WriteLine("Completed.");
chatClient.Dispose();
Console.ReadLine();


internal class AiResponse
{
    public string Filename { get; set; }
    public short Rating { get; set; }
    public string Reason { get; set; }
}