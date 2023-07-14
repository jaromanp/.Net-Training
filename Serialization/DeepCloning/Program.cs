using DeepCloning;
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

        Department departmentClone = department.Clone() as Department;

        Console.WriteLine("Original Department Object: ");
        Console.WriteLine(department.ToString());

        department.Name = "Testing";
        department.Employees.RemoveAt(2);
        department.Employees[1].EmployeeName = "Oscar";

        Console.WriteLine("Original modified Department Object: ");
        Console.WriteLine(department.ToString());
        Console.WriteLine("Cloned department Object: ");
        Console.WriteLine(departmentClone.ToString());
    }
}