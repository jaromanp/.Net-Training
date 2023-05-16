namespace FileSystemVisitor
{
    class Program
    {
        public static void Main(string[] args)
        {
            var visitor = new FileSystemVisitor(@"C:\Repos\C-sharp-Interfaces-and-generics-3152729-main");
            foreach (var entry in visitor.GetFileSystemEntries())
            {
                 Console.WriteLine((File.GetAttributes(entry) & FileAttributes.Directory) == FileAttributes.Directory ? "- " + entry : "* " + entry);
            }

            var visitor2 = new FileSystemVisitor(@"C:\", s => s.EndsWith(".txt"));
            foreach (var entry in visitor.GetFileSystemEntries())
            {
                Console.WriteLine(entry);
            }

            var visitor3 = new FileSystemVisitor(@"C:\", s => new FileInfo(s).Length > 1024 * 1024);
            foreach (var entry in visitor.GetFileSystemEntries())
            {
                Console.WriteLine(entry);
            }

            var visitor4 = new FileSystemVisitor(@"C:\", s => new FileInfo(s).CreationTime > DateTime.Now.AddDays(-7));
            foreach (var entry in visitor.GetFileSystemEntries())
            {
                Console.WriteLine(entry);
            }
        }
    }
}
