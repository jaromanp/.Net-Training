namespace FileSystemVisitorTest
{
    [TestClass]
    public class FileSystemVisitorTests
    {
        [TestMethod]
        public void Traverse_ReturnsAllFiles()
        {
            var path = @"C:\TestFolder";
            var visitor = new FileSystemVisitor.FileSystemVisitor(path, s => true);

            Directory.CreateDirectory(path);
            File.WriteAllText(Path.Combine(path, "file1.txt"), "Test file 1");
            File.WriteAllText(Path.Combine(path, "file2.txt"), "Test file 2");
            Directory.CreateDirectory(Path.Combine(path, "subdir"));
            File.WriteAllText(Path.Combine(path, "subdir", "file3.txt"), "Test file 3");

            var files = visitor.Traverse().ToList();

            Assert.AreEqual(3, files.Count);

            Directory.Delete(path, true);
        }

        [TestMethod]
        public void Traverse_FiltersByFileName()
        {
            var path = @"C:\TestFolder";
            var visitor = new FileSystemVisitor.FileSystemVisitor(path, s => !s.Contains("file2"));

            Directory.CreateDirectory(path);
            File.WriteAllText(Path.Combine(path, "file1.txt"), "Test file 1");
            File.WriteAllText(Path.Combine(path, "file2.txt"), "Test file 2");
            Directory.CreateDirectory(Path.Combine(path, "subdir"));
            File.WriteAllText(Path.Combine(path, "subdir", "file3.txt"), "Test file 3");

            var files = visitor.Traverse().ToList();

            Assert.AreEqual(2, files.Count);

            Directory.Delete(path, true);
        }

        [TestMethod]
        public void Traverse_FiltersByFileType()
        {
            var path = @"C:\TestFolder";
            var visitor = new FileSystemVisitor.FileSystemVisitor(path, s => Path.GetExtension(s) == ".txt");

            Directory.CreateDirectory(path);
            File.WriteAllText(Path.Combine(path, "file1.txt"), "Test file 1");
            File.WriteAllText(Path.Combine(path, "file2.jpg"), "Test file 2");
            File.WriteAllText(Path.Combine(path, "file3.txt"), "Test file 3");

            var files = visitor.Traverse().ToList();

            Assert.AreEqual(2, files.Count);

            Directory.Delete(path, true);
        }
    }

}