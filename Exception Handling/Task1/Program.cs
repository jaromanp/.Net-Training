using System;
using System.Runtime.CompilerServices;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter a string: ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        throw new Exception("Input cannot be empty");
                    }

                    if (input.ToLower() == "exit")
                    {
                        break;
                    }

                    Console.WriteLine(input[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}