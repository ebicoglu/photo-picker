using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//////// BUILD THE HOST
var builder = Host.CreateApplicationBuilder();
builder
    .Services
    .AddChatClient(new OllamaChatClient(
        endpoint: new Uri("http://localhost:11434"),
        modelId: "llava"))
    .Build();

var app = builder.Build();

//////// CREATE THE CHAT CLIENT
var chatClient = app.Services.GetRequiredService<IChatClient>();

//////// CREATE MESSAGE
var messages = new List<ChatMessage>
{
    //new ChatMessage {
    //    Role = ChatRole.System,
    //    Text = "You are an agent that analyzes images for social platforms. "
    //}
};


//messages.Add(new ChatMessage
//{
//    Role = ChatRole.User,
//    Text = "Describe these photos: \n" +
//           "https://raw.githubusercontent.com/ebicoglu/photo-picker/refs/heads/main/PhotoPicker/photos/photo-1.jpg, \n" +
//           "https://raw.githubusercontent.com/ebicoglu/photo-picker/refs/heads/main/PhotoPicker/photos/photo-2.jpg, \n" +
//           "https://raw.githubusercontent.com/ebicoglu/photo-picker/refs/heads/main/PhotoPicker/photos/photo-3.jpg, \n" +
//           "https://raw.githubusercontent.com/ebicoglu/photo-picker/refs/heads/main/PhotoPicker/photos/photo-4.jpg"
//});

 

//////// ADD IMAGES TO THE MESSAGE
var contents = new List<AIContent>();
const string directory = @"D:\github\alper\photo-picker\PhotoPicker\photos";
var files = Directory.GetFiles(directory, "*.jpg");
for (var i = 0; i < files.Length; i++)
{
    var file = files[i];
    messages.Add(new ChatMessage
    {
        Role = ChatRole.User,
        Text = "Describe this photo",
        Contents = new List<AIContent> { new ImageContent(File.ReadAllBytes(file), "image/jpeg") }
    });
}

//////// ASK TO AI
Console.WriteLine(string.Join("\n", messages.Select(x => x.Text).Where(x => x != null)) + "\n\r");
var chatResponse = await chatClient.CompleteAsync(messages);
messages.Add(chatResponse.Message);

Console.WriteLine(chatResponse.Message.Text);


Console.Write("You > ");
var input = Console.ReadLine();

while (input != "exit")
{
    messages.Add(new ChatMessage { Role = ChatRole.User, Text = input });
    chatResponse = await chatClient.CompleteAsync(messages);
    messages.Add(chatResponse.Message);
    Console.WriteLine("\nAI > " + chatResponse.Message.Text);
    Console.Write("\nYou > ");
    input = Console.ReadLine();
}

