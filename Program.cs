
namespace TaskTracker
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Task Tracker!");
            var taskHelper = new TasksHelper ();
            //taskHelper.LoadTasks();
            TasksHelper.CommandController();
        }
    }
}