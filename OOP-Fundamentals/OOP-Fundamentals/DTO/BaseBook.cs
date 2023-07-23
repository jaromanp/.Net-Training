using OOP_Fundamentals.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.DTO
{
    public class BaseBook : IDocument
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime DatePublished { get; set; }
        public DocumentType Type { get; set; }

        public virtual string GetCardInfo()
        {
            return $"Book: {Title} by {Authors}"; 
        }
    }
}
