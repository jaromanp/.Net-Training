using OOP_Fundamentals.Interface;
using OOP_Fundamentals.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.Service
{
    class SearchService
    {
        private readonly DocumentRepository _documentRepository;

        public SearchService(DocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public void SaveDocument(IDocument document, string documentNumber)
        {
            _documentRepository.SaveDocument(document, documentNumber);
        }

        public IEnumerable<IDocument> SearchByDocumentNumber(string documentNumber)
        {
            return _documentRepository.SearchByDocumentNumber(documentNumber);
        }
    }
}
