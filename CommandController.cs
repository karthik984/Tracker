// See https://aka.ms/new-console-template for more information
namespace TaskTracker
{
    public class CommandController{
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

        public  void HandleCommands()
        {
            PrintAvailbleCommands();            
            string command;
            var taskHelper = new TaskHelper();

            do{
                Console.WriteLine("Please select a commad to continue..");
                command = Console.ReadLine().Trim();
                    taskHelper.TaskActionController(command);
            }while(command != "exit" && command != "quit");
            Console.WriteLine("bye..bye!");
        }
    }
}

