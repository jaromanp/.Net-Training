using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_serialization
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class Department
    {
        [JsonProperty("department_name")]
        public string Name { get; set; }

        [JsonProperty]
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
