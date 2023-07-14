using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML_serialization
{
    [Serializable]
    public class Department
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlArrayItem("Employee")]
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
    }
}
