using Binary_serialization;
using System;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    static void Main(string[] args)
    {
        Department department = new Department()
        {
            Name = "Engineering",
            Employees = new List<Employee>()
            {
                new Employee() { EmployeeName = "Alex" },
                new Employee() { EmployeeName = "Maria" },
                new Employee() { EmployeeName = "Albert" }
            }
        };

        Console.WriteLine("Original object:");
        Console.WriteLine(department.ToString());

        Console.WriteLine("Now let's serialize the object into a file");
        Stream stream = File.Open("department.bin", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, department);
        stream.Close();

        Console.WriteLine("Clear the department object");
        department = null;

        stream = File.Open("department.bin", FileMode.Open);
        formatter = new BinaryFormatter();
        department = (Department)formatter.Deserialize(stream);
        stream.Close();
        Console.WriteLine("Deserialize object:");
        Console.WriteLine(department.ToString());
    }
}