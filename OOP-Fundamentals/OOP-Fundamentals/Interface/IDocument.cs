using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.Interface
{
    interface IDocument
    {
        string Title { get; set; }
        DateTime DatePublished { get; set; }
        DocumentType Type { get; set; }
        string GetCardInfo();
    }
}
