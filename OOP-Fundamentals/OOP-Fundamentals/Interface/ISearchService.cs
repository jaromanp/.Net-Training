using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.Interface
{
    interface ISearchService
    {
        IEnumerable<IDocument> SearchByDocumentNumber(string documentNumber);
        void SaveDocument(IDocument document, string documentNumber);
    }
}
