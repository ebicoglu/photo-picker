﻿using System.Text.Json;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

////////   1.) BUILD THE DEPENDENCY INJECTION   //////// 
var builder = Host.CreateApplicationBuilder();
//var chatClient = builder.Services.AddChatClient(new OpenAIChatClient(new OpenAIClient(Environment.GetEnvironmentVariable("OPENAI_API_KEY")), "gpt-4o")).Build();
var chatClient = builder.Services.AddChatClient(new OllamaChatClient(endpoint: new Uri("http://localhost:11434"), modelId: "llama3.2-vision")).Build();


////////   2.) CREATE THE PROMPTS    ////////
var images = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\photos")).GetFiles("*.jpg");
var aiResponses = new List<AiResponse>(images.Length);
foreach (var image in images)
{
    var messages = new List<ChatMessage>
    {
        new ChatMessage
        {
            Role = ChatRole.System,
            Text = @"
                    You are an AI model that must respond in **strict JSON format**.
                    Analyze images for Instagram aesthetics on a scale from 1 to 100, considering composition, lighting, and engagement potential.
                    Always return a **valid, fully enclosed JSON object** in the exact format:
                    {         
                      ""rating"": <number between 1 and 100>,
                      ""reason"": ""<Brief explanation of why the rating was given>""
                    }

                    ### **Rules:**
                    1. **Return ONLY JSON** —do not add any extra text before or after.
                    2. **Ensure the JSON is properly enclosed with `{}` brackets**.
                    3. **If you generate an invalid JSON, fix yourself and return a valid JSON.**
                    4. **If your response is wrapped in backticks, correct yourself and return a valid JSON immediately.**

                    Example output:
                    {         
                        ""rating"": 85,
                        ""reason"": ""The image has strong composition and lighting.""
                    }
               "
        },
        new ChatMessage
        {
            Role = ChatRole.User,
            Text =  "Analyze this image",
            Contents = new List<AIContent> { new ImageContent(File.ReadAllBytes(image.FullName), "image/jpeg") }
        }
    };


    ////////   3.) ASK TO AI   ////////
    Console.WriteLine("» ASK TO AI: " + image.Name);
    var chatResponse = await chatClient.CompleteAsync(messages);
    Console.WriteLine("» AI-RESPONSE: \n" + chatResponse.Message.Text + "\n" + new string('═', 100));


    ////////   4.) DESERIALIZE THE AI RESPONSE   ////////
    if (TryParseJson<AiResponse>(chatResponse.Message.Text, out var aiResponse))
    {
        aiResponse.Filename = image.Name;
        aiResponses.Add(aiResponse);
    }
}


//////   5.) FIND THE WINNER   ////////
var winner = aiResponses.MaxBy(x => x.Rating);
Console.WriteLine("»»» WINNER: " + winner?.Filename + " «««");
Console.ReadLine();



bool TryParseJson<T>(string? text, out T? result)
{
    try
    {
        text = text
            .Trim()
            .TrimStart("\"```json\"".ToCharArray())
            .TrimEnd("```".ToCharArray()).Trim();

        var r = JsonSerializer.Deserialize<dynamic>(text);

        result = JsonSerializer.Deserialize<T>(text, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        });

        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        result = default(T);
        return false;
    }
}


internal class AiResponse
{
    public string Filename { get; set; }
    public byte Rating { get; set; }
    public string Reason { get; set; }
}

