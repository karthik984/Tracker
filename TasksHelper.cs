// See https://aka.ms/new-console-template for more information
using System.Text.Json;

namespace TaskTracker
{
    public class TasksHelper
    {
        private static List<Task> tasks = new();
        public TasksHelper()
        {
            LoadTasks();
        }
        private Dictionary<string, Action<string>> commandActions = new Dictionary<string, Action>
        {
            {"add", AddTask},
            {"update", UpdateTask},
            {"view", TaskInfo},
            {"list", PrintTasks},
            {"pending", GetPendingTasks},
            {"overdue", GetOverdueTasks},
            {"delete", DeleteTask},
            {"done", GetCompletedTask},
            {"quit", ExitTask},
            {"exit", ExitTask},
            {"help", GetCommands}
            // you can add other commands and actions ...

        };

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

        public static List<Task> Tasks { get => Tasks1; set => Tasks1 = value; }
        public static List<Task> Tasks1 { get => tasks; set => tasks = value; }
        public static List<Task> Tasks2 { get => tasks; set => tasks = value; }

        private static void TaskInfo()
        {
            throw new NotImplementedException();
        }

        private static void UpdateTask()
        {
            throw new NotImplementedException();
        }

        private static void AddTask()
        {
            throw new NotImplementedException();
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

