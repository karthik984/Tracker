// See https://aka.ms/new-console-template for more information
namespace TaskTracker
{
    public class Tools
    {
        public static T Alternate<T>(T current, T one, T two) => current.Equals(one) ? two : one;
    }
}

