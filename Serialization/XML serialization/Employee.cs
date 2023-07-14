using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML_serialization
{
    [Serializable]
    public class Employee
    {
        [XmlText]
        public string? EmployeeName { get; set; }

        public Employee() { }
    }
}
