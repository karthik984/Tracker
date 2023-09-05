// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;
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

        private static void ExitApp()
        {
            throw new NotImplementedException();
        }

        private static void GetCompletedTask()
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

        private static void TaskInfo(int index)
        {
            Task task = Tasks[index];
            Console.WriteLine($"S.No.: {task.Id}");
            Console.WriteLine($"Task: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Due: {task.Due}");
            Console.WriteLine($"Completed: {(task.Done == null ? "Not Done" : task.Done)}");
            Console.WriteLine($"Last Updated: {task.LastUpdated}");    
        }

        private static void ProcessUpdate(int index)
        {
            Task task;

            try
            {
                task = Tasks[index];    
            }
            catch (System.Exception)
            {
                
                throw new Exception($"No task exists at index {index}");
            }
                        
            Console.WriteLine("What's the new name of the task?");
            var new_name = Console.ReadLine() ?? task.Name ;
            Console.WriteLine("the new description?");
            var new_desc = Console.ReadLine() ?? task.Description;
            Console.WriteLine("When's it due?");
            var new_due = DateTime.Parse(Console.ReadLine());

            new_due = (new_due == null)? task.Due: new_due;

            PutTask(index, new_name, new_desc, new_due);
        }

        private static void PutTask(int id, string taskName, string taskDesc, DateTime due)
        {
            var task = new Task(id, taskName, taskDesc, due);
            Json.saveTask(task, jsonFile);
        }

        public static void ProcessAdd()
        {
                    Console.WriteLine("Please enter the task name: ");
                    var taskName = Console.ReadLine();
                    Console.WriteLine("Please enter the task description: ");
                    var taskDesc = Console.ReadLine();
                    Console.WriteLine("When's it due: ");
                    var due = DateTime.Parse(Console.ReadLine());
                    var id = Tasks.Max(t => t.Id) + 1;

                    PutTask(id, taskName, taskDesc, due);
        }

        public static void Controller(string command)
        {
            int index;
            switch (command)
            {
                case "add":
                    ProcessAdd();
                    break;
                
                case "update":
                    Console.WriteLine("Enter the Task Number which you want to update:");
                    index = int.Parse(Console.ReadLine());                    
                    ProcessUpdate(index);                    
                    break;
                
                case "delete":
                    Console.WriteLine("Enter the Task Number to remove");
                    index = int.Parse(Console.ReadLine());
                    DeleteTask(index);
                    break;
                
                case "view":
                    Console.WriteLine("Enter the task info that you want to view");
                    index = int.Parse(Console.ReadLine());
                    TaskInfo(index);
                    break;
                default:
                    Console.WriteLine("Please select a commad to continue..");
                break;
            }

        }

        private static void DeleteTask(int index)
        {
            try
            {
                
                var task = Tasks[index];
            }
            catch (System.Exception)
            {
                
                throw new Exception($"No task exists at index {index}");
            }
            string jsonFilePath = "path_to_your_json_file.json";
            string jsonContent = File.ReadAllText(jsonFilePath);

            using (var reader = new StreamReader("./tracker.json"))
            {
                string jsonContent = reader.ReadToEnd();
                if (jsonContent != null)
                {
                    Tasks = JsonSerializer.Deserialize<List<Task>>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
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

        public static void ListTasks()
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

