using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_serialization
{
    [Serializable]
    public class Employee
    {
        [JsonProperty("employee_name")]
        public string? EmployeeName { get; set; }

        public Employee() { }
    }
}
