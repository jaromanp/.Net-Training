using Newtonsoft.Json;
using OOP_Fundamentals.DTO;
using OOP_Fundamentals.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.Repository
{
    class DocumentRepository : IDocumentRepository
    {
        private readonly ICacheService _cacheService;

        public DocumentRepository(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

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

                    IDocument document = _cacheService.GetFromCache(filePath);

                    if (document == null)
                    {

                        string content = File.ReadAllText(filePath);

                        switch (documentType)
                        {
                            case "book":
                                document = JsonConvert.DeserializeObject<Book>(content);
                                break;
                            case "localizedbook":
                                document = JsonConvert.DeserializeObject<LocalizedBook>(content);
                                break;
                            case "patent":
                                document = JsonConvert.DeserializeObject<Patent>(content);
                                break;
                            case "magazine":
                                document = JsonConvert.DeserializeObject<Magazine>(content);
                                break;
                        }

                        TimeSpan cacheDuration = GetCacheDuration(document.GetType().Name);
                        if (cacheDuration > TimeSpan.Zero)
                        {
                            _cacheService.AddToCache(filePath, document, cacheDuration);
                        }
                    }

                    results.Add(document);
                }
            }

            return results;
        }

        private TimeSpan GetCacheDuration(string documentType)
        {
            return documentType switch
            {
                "Book" => TimeSpan.FromHours(2),
                "LocalizedBook" => TimeSpan.FromMinutes(5),
                "Patent" => TimeSpan.FromDays(1),
                "Magazine" => TimeSpan.Zero,
                _ => TimeSpan.Zero,
            };
        }
    }
}
