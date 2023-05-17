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
                
                if(!input.Equals("exit"))
                {
                    if (input.Equals("1"))
                    {
                        Console.WriteLine(@"Please provide the file path to visit. (I've already put C:\ so you don't have to and can start from there");
                        string filePath = Console.ReadLine();
                        Console.WriteLine("Now select the filter you want to implement");
                        Console.WriteLine("0. No Filter");
                        Console.WriteLine("1. File Type");
                        Console.WriteLine("2. File Name");
                        Console.WriteLine("3. Date");
                        string filter = Console.ReadLine();
                        switch (filter)
                        {
                            case "0":
                                PrintFiles(Processor(filePath, 0, ""));
                                break;
                            case "1":
                                Console.Write("Enter the file extension: ");
                                string fileType = Console.ReadLine();
                                PrintFiles(Processor(filePath, 1, fileType));
                                break;
                            case "2":
                                Console.Write("Enter the file or folder name: ");
                                string fileName = Console.ReadLine();
                                PrintFiles(Processor(filePath, 2, fileName));
                                break;
                            case "3":
                                Console.Write("Enter the creation date from wich you want to filter: ");
                                string date = Console.ReadLine();
                                PrintFiles(Processor(filePath, 3, date));
                                break;
                        }
                    } else
                    {
                        Console.WriteLine("Hmm, you seem to have entered an uknown input. Try again or 'exit'");
                    }      
                } 
            } while (input != "exit");                     
        }

        public static IEnumerable<String> Processor(string route, int filterType, string? filterBy){
            FileSystemVisitor visitor;
            switch (filterType) {
                case 1:
                    visitor = new FileSystemVisitor(@"C:\" + route, s => s.EndsWith(filterBy));
                    return visitor.GetFileSystemEntries();
                case 2:
                    visitor = new FileSystemVisitor(@"C:\" + route, s => s.Contains(filterBy));
                    return visitor.GetFileSystemEntries();               
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
                    visitor = new FileSystemVisitor(@"C:\" + route, s => new FileInfo(s).CreationTime > date);
                    return visitor.GetFileSystemEntries();               

            }
            visitor = new FileSystemVisitor(@"C:\" + route);
            return visitor.GetFileSystemEntries();
        }

        public static void PrintFiles(IEnumerable<string> files)
        {
            if (files.Count() > 0)
            {
                foreach (var entry in files)
                {
                    Console.WriteLine(entry);
                }
            } else { Console.WriteLine("No items were found"); }

        }
    }
}
