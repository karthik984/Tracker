// See https://aka.ms/new-console-template for more information
namespace TaskTracker
{
    public partial class TasksHelper
    {
        private static List<Task> _tasks = new();
        private static string jsonFile = "./tracker.json";
        private static void Save() => JsonFunctions.SaveToJson(_tasks, jsonFile);
        private static void PrintAvailbleCommands() =>
            Console.WriteLine(@"Available commands:
                        list - Lists tasks
                        view - See more info about a task by number
                        add - Creates a task
                        update - Updates a specific task
                        incomplete - Lists incomplete tasks
                        overdue - Lists overdue tasks
                        delete - Deletes a task by number
                        remaining - Gets the remaining time of a task by number
                        finish - Marks a task complete by number
                        clear - Clear the terminal
                        quit - Terminates the program
                        exit - Terminates the program
                        help - Shows this list again");


        public TasksHelper()
        {
            var jsonFunctions = new JsonFunctions();
            _tasks = jsonFunctions.LoadFromJson(jsonFile).ToList();
        }

        public static void CommandController()
        {
            PrintAvailbleCommands();
            
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
            int index = 0;

            if (command == "view" || command == "add" || command == "update" || 
                command == "finish" || command == "remaining")
            {
                Console.WriteLine($"Enter the Task Number on which you want to perform the command:");
                index = int.Parse(Console.ReadLine());
                index--;
                
                if(index <= 0 || index >_tasks.Count()-1)
                {
                    Console.WriteLine($"No task exists at index {index + 1}");
                    Console.WriteLine($"Please select number between 1 and {_tasks.Count()} ");
                    return;
                }
            }

            switch (command)
            {
                case "list":
                    GetTasks(_tasks);
                    break;
                
                case "view":
                    GetTask(index);
                    break;
                
                case "add":
                    ProcessAdd();
                    break;
                
                case "update":
                    ProcessUpdate(index);                    
                    break;
                
                case "delete":
                    DeleteTask(index);
                    break;

                case "incomplete":
                    var incompleteTasks = _tasks.Where(t => t.Done == null).ToList();
                    GetTasks(incompleteTasks);
                    break;
                
                case "completed":
                    var completedTasks = _tasks.Where(t => t.Done != null).ToList();
                    GetTasks(completedTasks);
                    break;

                case "finish":
                    _tasks[index].Done = DateTime.Now;
                    Save();
                    break;
                
                case "overdue":
                    var overdueTasks = _tasks.Where(t => t.Due < DateTime.Now && t.Done == null).ToList();
                    GetTasks(overdueTasks);
                    break;
                case "remaining":
                    PrintRemainingTime(index);                    
                    break;

                case "clear":
                    Console.Clear();
                    break;
            }
        }

        public static void GetTasks(List<Task> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks to print");
                return;
            }
            var defaultColor = Console.ForegroundColor;
            foreach (Task task in tasks)
            {
                Console.ForegroundColor = Tools.Alternate(Console.ForegroundColor, ConsoleColor.DarkGreen, ConsoleColor.DarkBlue);
                Console.WriteLine($"Task {task.Id}: {task.Name}");
                Console.WriteLine($"Due: {task.Due}");
                Console.WriteLine($"Completed: {(task.Done == null ? "Not Done" : task.Done)}");
                Console.WriteLine();
            }
            Console.ForegroundColor = defaultColor;
            
        }

        private static void GetTask(int index)
        {
            Task task = _tasks[index];
            Console.WriteLine($"S.No.: {task.Id}");
            Console.WriteLine($"Task: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Due: {task.Due}");
            Console.WriteLine($"Completed: {(task.Done == null ? "Not Done" : task.Done)}");
            Console.WriteLine($"Last Updated: {task.LastUpdated}");    
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

        private static void ProcessUpdate(int index)
        {
            var task = _tasks[index];    
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
            foreach(var task in _tasks.Skip(index + 1))
                task.Id--;

            _tasks.Remove(_tasks[index]);
            Console.WriteLine($"Task {index} has been removed.");

            Save();        
        }

        private static void PrintRemainingTime(int index) //does not work properly
        {
            if(_tasks[index].Done != null)
            {
                Console.WriteLine("Task has already been marked done");
                return;
            }          
            
            var timeDifference = _tasks[index].Due - DateTime.Now;
            var hours = Math.Abs(timeDifference.Hours);
            var minutes = Math.Abs(timeDifference.Minutes);

            var status = (timeDifference < TimeSpan.Zero) ? "Overdue" : "Due in";
            Console.WriteLine($"Task {status} {hours} hrs and {minutes} mins");
        }        
    }
}

