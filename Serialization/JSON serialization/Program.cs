using System;
using Newtonsoft.Json;
using JSON_serialization;

class Program
{
    static void Main(string[] args)
    {
        Department department = new Department()
        {
            Name = "Logistics",
            Employees = new List<Employee>()
            {
                new Employee() { EmployeeName = "Edwin" },
                new Employee() { EmployeeName = "Arturo" },
                new Employee() { EmployeeName = "Santiago" }
            }
        };

        Console.WriteLine("Original object:");
        Console.WriteLine(department.ToString());

        Console.WriteLine("Now let's serialize the object into a file");
        string jsonString = JsonConvert.SerializeObject(department);

        File.WriteAllText("department.json", jsonString);

        Console.WriteLine("Clear the department object");
        department = null;

        Console.WriteLine("Deserialize object:");
        department = JsonConvert.DeserializeObject<Department>(File.ReadAllText("department.json"));
        Console.WriteLine(department.ToString());
    }
}