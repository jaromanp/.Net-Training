using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DeepCloning
{
    [Serializable]
    public class Department : ICloneable
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }

        public Department() { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Department Name: {0}\n", Name);
            sb.AppendLine("Employees:");
            foreach (Employee employee in Employees)
            {
                sb.AppendFormat("\t{0}\n", employee.EmployeeName);
            }
            return sb.ToString();
        }

        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }
    }
}
