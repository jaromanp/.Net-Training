using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.Interface
{
    interface IDocument
    {
        string Authors { get; set; }
        DocumentType Type { get; set; }
        string GetCardInfo();
    }
}
