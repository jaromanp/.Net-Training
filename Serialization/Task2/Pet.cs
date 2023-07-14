using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    [Serializable]
    public class Pet : ISerializable
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Pet() { }

        public override string ToString()
        {            
            return String.Format("My Pet Name is {0} and it's {1} years old", Name, Age);
        }

        protected Pet(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Age = (int)info.GetValue("Age", typeof(int));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Age", Age);
        }
    }
}
