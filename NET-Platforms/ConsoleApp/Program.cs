using System;
using ClassLibrary;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            Console.WriteLine("Please enter your username");
            name = Console.ReadLine();
            string output = ConcatLibrary.GetHelloWorld(name);
            Console.WriteLine(output);
        }
    }
}
