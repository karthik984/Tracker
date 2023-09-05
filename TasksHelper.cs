using System.Linq.Expressions;
// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Xml.Linq;

namespace TaskTracker
{
    public partial class TasksHelper
    {
        private static List<Task> _tasks = new();
        private static string jsonFile = "./tracker.json";
        private static void Save() => JsonFunctions.SaveToJson(_tasks, jsonFile);

        public TasksHelper()
        {
            var jsonFunctions = new JsonFunctions();
            _tasks = jsonFunctions.LoadFromJson(jsonFile).ToList();
        }

        public static void CommandController()
        {
            Console.WriteLine(@"Available commands:
            list - Lists tasks
            add - Creates a task
            update - Updates a specific task
            view - See more info about a task by number
            incomplete - Lists incomplete tasks
            overdue - Lists overdue tasks
            delete - Deletes a task by number
            remaining - Gets the remaining time of a task by number
            done - Marks a task complete by number
            quit - Terminates the program
            exit - Terminates the program
            help - Shows this list again");

            string command;
            
            do{
                Console.WriteLine("Please select a commad to continue..");
                command = Console.ReadLine().Trim();
                    TaskActionController(command);
            }while(command != "exit" && command != "quit");
            System.Console.WriteLine("bye..bye!");
        }

        public static void TaskActionController(string command)
        {
            int index;
            switch (command)
            {
                case "list":
                    GetAllTasks();
                    break;
                
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
            }

        }

        public static void GetAllTasks()
        {
            if (_tasks.Count == 0)
            {
                Console.WriteLine("No tasks to print");
                return;
            }

            foreach (Task task in _tasks)
            {
                Console.WriteLine($"Task: {task.Name}");
                Console.WriteLine($"Due: {task.Due}");
                Console.WriteLine($"Completed: {(task.Done == null ? "Not Done" : task.Done)}");
            }
        }
        public static void ProcessAdd()
        {
            Console.WriteLine("Please enter the task name: ");
            var taskName = Console.ReadLine();
            Console.WriteLine("Please enter the task description: ");
            var taskDesc = Console.ReadLine();
            Console.WriteLine("When's it due: ");
            var due = DateTime.Parse(Console.ReadLine());
            var id = _tasks.Max(t => t.Id) + 1;
            
            var task = new Task(id, taskName, taskDesc, due);
            _tasks.Add(task);
            Save();
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
            Task task = _tasks[index];
            Console.WriteLine($"S.No.: {task.Id}");
            Console.WriteLine($"Task: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Due: {task.Due}");
            Console.WriteLine($"Completed: {(task.Done == null ? "Not Done" : task.Done)}");
            Console.WriteLine($"Last Updated: {task.LastUpdated}");    
        }

        private static void ProcessUpdate(int index)
        {
            try
            {
                var task = _tasks[index];    
            }
            catch (System.Exception)
            {
                
                throw new Exception($"No task exists at index {index}");
            }
                        
            Console.WriteLine("What's the new name of the task?");
            _tasks[index].Name  = Console.ReadLine() ?? _tasks[index].Name;
            Console.WriteLine("the new description?");
            _tasks[index].Description = Console.ReadLine() ?? _tasks[index].Description;
            Console.WriteLine("When's it due?");
            var new_due = DateTime.Parse(Console.ReadLine());

            _tasks[index].Due = (new_due == null)? _tasks[index].Due: new_due;
            JsonFunctions.SaveToJson(_tasks, jsonFile);
        }

        private static void DeleteTask(int index)
        {
            try
            {
                
                var task = _tasks[index];
            }
            catch (System.Exception)
            {
                
                throw new Exception($"No task exists at index {index}");
            }

            using (var reader = new StreamReader(jsonFile))
            {
                string jsonContent = reader.ReadToEnd();
                if (jsonContent != null)
                {
                    _tasks = JsonSerializer.Deserialize<List<Task>>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }

        }
        
    }
}

