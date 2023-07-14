using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using XML_serialization;

class Program
{
    static void Main(string[] args)
    {
        Department department = new Department()
        {
            Name = "Testing",
            Employees = new List<Employee>()
            {
                new Employee() { EmployeeName = "Pedro" },
                new Employee() { EmployeeName = "Alan" },
                new Employee() { EmployeeName = "Hugo" }
            }
        };

        Console.WriteLine("Original object:");
        Console.WriteLine(department.ToString());

        Console.WriteLine("Now let's serialize the object into a file");
        string path = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(path, "department.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Department));

        using(TextWriter tw = new StreamWriter(filePath))
        {
            xmlSerializer.Serialize(tw, department);
        }

        Console.WriteLine("Clear the department object");
        department = null;

        XmlSerializer deserializer = new XmlSerializer(typeof(Department));
        TextReader reader = new StreamReader(filePath);
        object obj = deserializer.Deserialize(reader);
        department = (Department)obj;
        reader.Close();

        Console.WriteLine("Deserialize object:");
        Console.WriteLine(department.ToString());
    }
}