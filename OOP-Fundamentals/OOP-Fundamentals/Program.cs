using OOP_Fundamentals.DTO;
using OOP_Fundamentals.Interface;
using OOP_Fundamentals.Repository;
using OOP_Fundamentals.Service;


DocumentRepository documentRepository = new DocumentRepository();
SearchService searchService = new SearchService(documentRepository);

Console.WriteLine("Cabinet Software for library");

Book book = new Book("1234567890", "Moby-Dick", 504, "Herman Melville", "Sample Publisher", DateTime.Parse("1851-10-18"));
Patent patent = new Patent("Apple Computers", "Steve Jobs", DateTime.Parse("1990-1-1"), DateTime.Parse("2010-1-31"), OOP_Fundamentals.DocumentType.Patent);
LocalizedBook book2 = new LocalizedBook("9788439719786", "100 años de soledad", 504, "Gabriel Garcia Marquez", "Sample Publisher", "Colombia", "Editorial Planeta", DateTime.Parse("1967-05-1"));
Magazine magazine = new Magazine("Time", "New York Times", 200, DateTime.Now);

List<IDocument> documents = new List<IDocument>();
documents.Add(book);
documents.Add(book2);
documents.Add(patent);
documents.Add(magazine);

for(int i = 0; i < documents.Count;  i++)
{
    searchService.SaveDocument(documents[i], "1");
}

var results = searchService.SearchByDocumentNumber("1");

Console.WriteLine("Search results:");
foreach (var result in results)
{
    Console.WriteLine(result.GetCardInfo());
}