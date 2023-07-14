using System.Runtime.Serialization.Formatters.Binary;
using Task2;

class Program
{
    static void Main(string[] args)
    {
        Pet kitty = new Pet() { Name = "Snowbell", Age = 8};
       
        Console.WriteLine("Original object:");
        Console.WriteLine(kitty.ToString());

        Console.WriteLine("Now let's serialize the object into a file");
        MemoryStream stream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, kitty);

        Console.WriteLine("Clear the kitty object");
        kitty = null;

        Console.WriteLine("Deserialize object:");
        stream.Seek(0, SeekOrigin.Begin);
        kitty = (Pet)formatter.Deserialize(stream);

        Console.WriteLine(kitty.ToString());
    }
}