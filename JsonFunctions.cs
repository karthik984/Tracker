// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskTracker
{
    public class JsonFunctions{
    public static void SaveToJson(List<Task> tasks, string jsonFilePath)
    {
        string json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(jsonFilePath, json);
    }

    public IEnumerable<Task> LoadFromJson(string jsonFilePath)
    {
        List<Task> tasks = new();

        Console.WriteLine("Loading task list from a json file:");
        using (var reader = new StreamReader("./tracker.json"))
        {
            string jsonContent = reader.ReadToEnd();
            if (jsonContent != null)
            {
                tasks = JsonSerializer.Deserialize<List<Task>>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
        
        return tasks;
    }
}
}

