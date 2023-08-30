// See https://aka.ms/new-console-template for more information
using System.Text.Json;

namespace TaskTracker
{
    public partial class TasksHelper
    {
        private static List<Task> Tasks = new();
        private static string jsonFile = "./tracker.json";

        public TasksHelper()
        {
            
        }

        private static void ExitTask()
        {
            throw new NotImplementedException();
        }

        private static void GetCompletedTask()
        {
            throw new NotImplementedException();
        }

        private static void DeleteTask(string name)
        {
            throw new NotImplementedException();
        }

        private static void GetOverdueTasks()
        {
            throw new NotImplementedException();
        }

        private static void GetPendingTasks()
        {
            throw new NotImplementedException();
        }

        private static void TaskInfo()
        {
            throw new NotImplementedException();
        }

        private static void UpdateTask()
        {
            throw new NotImplementedException();
        }

        private static void AddTask(string taskName, string taskDesc, DateTime due)
        {
            var task = new Task(taskName, taskDesc, due);
            Json.saveTask(task, jsonFile);
        }


        public static void Controller(string command)
        {
            switch (command)
            {
                case "add":
                    Console.WriteLine("Please enter the task name: ");
                    var taskName = Console.ReadLine();                    
                    Console.WriteLine("Please enter the task description: ");
                    var taskDesc = Console.ReadLine();
                    Console.WriteLine("When's it due: ");
                    var due = DateTime.Parse(Console.ReadLine());
                    AddTask(taskName, taskDesc, due);
                break;
                default:
                    Console.WriteLine("Please select a commad to continue..");
                break;
            }

        }

        public static void GetCommands()
        {
            Console.WriteLine(@"Please select one of the commands below:
        add - Creates a task
        update - Updates a specific task
        view - See more info about a task by number
        list - Lists tasks
        incomplete - Lists incomplete tasks
        overdue - Lists overdue tasks
        delete - Deletes a task by number
        remaining - Gets the remaining time of a task by number
        done - Marks a task complete by number
        quit - Terminates the program
        exit - Terminates the program
        help - Shows this list again");
        }

        public void LoadTasks()
        {
            Console.WriteLine("Loading task list from a json file:");
            using (var reader = new StreamReader("./tracker.json"))
            {
                string jsonContent = reader.ReadToEnd();
                if (jsonContent != null)
                {
                    Tasks = JsonSerializer.Deserialize<List<Task>>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
        }

        public static void PrintTasks()
        {
            if (Tasks.Count == 0)
            {
                Console.WriteLine("No tasks to print");
                return;
            }

            foreach (Task task in Tasks)
            {
                Console.WriteLine($"Task: {task.Name}");
                Console.WriteLine($"Due: {task.Due}");
                Console.WriteLine($"Completed: {(task.Done == null ? "Not Done" : task.Done)}");
            }
        }
        
    }
}

