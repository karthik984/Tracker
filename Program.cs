
namespace TaskTracker
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Task Tracker!");
            var taskHelper = new TasksHelper ();
            //taskHelper.LoadTasks();
            TasksHelper.GetCommands();
        }
    }
}



// command_list = ["add", "view", "update", "list", "incomplete", "overdue", "delete", "remaining", "help", "quit", "exit", "done"]
// def print_options():
//     """ prints a readable list of commands that can be typed when prompted """
//     print("Choices")
//     print("add - Creates a task")
//     print("update - updates a specific task")
//     print("view - see more info about a task by number")
//     print("list - lists tasks")
//     print("incomplete - lists incomplete tasks")
//     print("overdue - lists overdue tasks")
//     print("delete - deletes a task by number")
//     print("remaining - gets the remaining time of a task by number")
//     print("done - marks a task complete by number")
//     print("quit or exit - terminates the program")
//     print("help - shows this list again")
