// See https://aka.ms/new-console-template for more information
using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.VisualBasic;

Console.WriteLine("Welcome to Task Tracker!");
load();

void load()
{
    Console.WriteLine("Loading task list from a json file:");
    using(var reader = new StreamReader("tracker.json"))
    {
        string jsonContent = reader.ReadToEnd();
        List<Task> tasks = new ();
        tasks = JsonSerializer.Deserialize<List<Task>>(jsonContent);
        printTasks(tasks);
    }
}

void printTasks(List<Task>? tasks)
{
    if(tasks.Count ==0)
    {
        System.Console.WriteLine("No tasks to print");
        return;
    }

    foreach(Task task in tasks)
    {
        System.Console.WriteLine($"task: {task.Name}");
        System.Console.WriteLine($"Due: {task.Due}");
        System.Console.WriteLine($"task: {task.Status}");
    }
}

public class Task 
{
    public string Name {get; set;}
    public DateTime Due { get; set; }
    public string Description { get; set; }
    public DateTime LastUpdated {get; set; }
    public bool Status {get; set; } = false;
}