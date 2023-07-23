using Newtonsoft.Json;
using OOP_Fundamentals.DTO;
using OOP_Fundamentals.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.Repository
{
    class DocumentRepository
    {
        public void SaveDocument(IDocument document, string documentNumber)
        {
            string documentJson = JsonConvert.SerializeObject(document);
            DocumentType type = document.GetType().Name switch
            {
                "Book" => DocumentType.Book,
                "LocalizedBook" => DocumentType.LocalizedBook,
                "Patent" => DocumentType.Patent,
                "Magazine" => DocumentType.Magazine,
                _ => throw new ArgumentException("Unsupported document type")
            };
            File.WriteAllText($"{type.ToString().ToLower()}_#{documentNumber}.json", documentJson);
        }

        public IEnumerable<IDocument> SearchByDocumentNumber(string documentNumber)
        {
            List<IDocument> results = new List<IDocument>();
            string[] documentTypes = { "book", "localizedbook", "patent", "magazine" };

            foreach (string documentType in documentTypes)
            {
                string filePath = $"{documentType}_#{documentNumber}.json";
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    switch (documentType)
                    {
                        case "book":
                            results.Add(JsonConvert.DeserializeObject<Book>(content));
                            break;
                        case "localizedbook":
                            results.Add(JsonConvert.DeserializeObject<LocalizedBook>(content));
                            break;
                        case "patent":
                            results.Add(JsonConvert.DeserializeObject<Patent>(content));
                            break;
                        case "magazine":
                            results.Add(JsonConvert.DeserializeObject<Magazine>(content));
                            break;
                    }
                }
            }

            return results;
        }
    }
}
