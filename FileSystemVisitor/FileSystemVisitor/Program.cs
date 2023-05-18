using System.IO;

namespace FileSystemVisitor
{
    class Program
    {
        public static void Main(string[] args)
        {
            string welcomeMessage = @"
            ░█░█░█▀▀░█░░░█▀▀░█▀█░█▄█░█▀▀░░░▀█▀░█▀█░░░█▀▀░▀█▀░█░░░█▀▀░█▀▀░█░█░█▀▀░▀█▀░█▀▀░█▄█░█░█░▀█▀░█▀▀░▀█▀░▀█▀░█▀█░█▀▄
            ░█▄█░█▀▀░█░░░█░░░█░█░█░█░█▀▀░░░░█░░█░█░░░█▀▀░░█░░█░░░█▀▀░▀▀█░░█░░▀▀█░░█░░█▀▀░█░█░▀▄▀░░█░░▀▀█░░█░░░█░░█░█░█▀▄
            ░▀░▀░▀▀▀░▀▀▀░▀▀▀░▀▀▀░▀░▀░▀▀▀░░░░▀░░▀▀▀░░░▀░░░▀▀▀░▀▀▀░▀▀▀░▀▀▀░░▀░░▀▀▀░░▀░░▀▀▀░▀░▀░░▀░░▀▀▀░▀▀▀░▀▀▀░░▀░░▀▀▀░▀░▀";
            Console.WriteLine(welcomeMessage);
            string input;

            do
            {
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Start");
                Console.WriteLine("*To Quit please type exit");
                input = Console.ReadLine();

                if (!input.Equals("exit"))
                {
                    if (input.Equals("1"))
                    {
                        Console.WriteLine(@"Please provide the file path to visit. (I've already put C:\ so you don't have to and can start from there");
                        string filePath = Console.ReadLine();
                        if (Directory.Exists(@"C:\" + filePath))
                        {
                            Console.WriteLine("Now select the filter you want to implement");
                            Console.WriteLine("0. No Filter");
                            Console.WriteLine("1. File Type");
                            Console.WriteLine("2. File Name");
                            Console.WriteLine("3. Date");
                            string filter = Console.ReadLine();
                            switch (filter)
                            {
                                case "0":
                                    Processor(filePath, 0, "");
                                    break;
                                case "1":
                                    Console.Write("Enter the file extension: ");
                                    string fileType = Console.ReadLine();
                                    Processor(filePath, 1, fileType);
                                    break;
                                case "2":
                                    Console.Write("Enter the file name: ");
                                    string fileName = Console.ReadLine();
                                    Processor(filePath, 2, fileName);
                                    break;
                                case "3":
                                    Console.Write("Enter the creation date from wich you want to filter: ");
                                    string date = Console.ReadLine();
                                    Processor(filePath, 3, date);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Hmm, you seem to have entered an uknown path. Try again or 'exit'");
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Hmm, you seem to have entered an uknown input. Try again or 'exit'");
                    }
                }
            } while (input != "exit");
        }

        public static void Processor(string route, int filterType, string? filterBy)
        {
            FileSystemVisitor visitor;
            switch (filterType)
            {
                case 1:
                    visitor = new FileSystemVisitor(@"C:\" + route, s => Path.GetExtension(s) == filterBy);                    
                    break;
                case 2:
                    visitor = new FileSystemVisitor(@"C:\" + route, s => Path.GetFileName(s).Contains(filterBy));
                    break;
                case 3:
                    DateTime date = DateTime.Now;
                    try
                    {
                        date = DateTime.Parse(filterBy);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Wrong input");
                    }
                    visitor = new FileSystemVisitor(@"C:\" + route, s => File.GetCreationTime(s) > date);
                    break;                
                default:
                    visitor = new FileSystemVisitor(@"C:\" + route);
                    break;
            }
            visitor.Start += path => Console.WriteLine($"Start: {path}");
            visitor.Finish += path => Console.WriteLine($"Finish: {path}");
            visitor.FileFound += path => Console.WriteLine($"File found: {path}");
            visitor.DirectoryFound += path => Console.WriteLine($"Directory found: {path}");
            visitor.FilteredFileFound += path => Console.WriteLine($"Filtered file found: {path}");
            visitor.FilteredDirectoryFound += path => Console.WriteLine($"Filtered directory found: {path}");            
            var files = visitor.Traverse().ToList();
            Console.WriteLine($"Total files found: {files.Count}");
            foreach (var file in files) Console.WriteLine(file);
        }
    }
}
