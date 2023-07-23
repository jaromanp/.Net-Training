using OOP_Fundamentals.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.DTO
{
    public class Patent : IDocument
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UniqueID { get; set; }
        public DocumentType Type { get; set; }

        public Patent(string title, string authors, DateTime datePublished, DateTime expirationDate, DocumentType type)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("The title is required.");
            Title = title;
            if (string.IsNullOrWhiteSpace(authors))
                throw new ArgumentException("The authors is required.");
            Authors = authors;
            DatePublished = datePublished;
            ExpirationDate = expirationDate;
            Type = type;
            UniqueID = Guid.NewGuid();
        }

        public string GetCardInfo()
        {
            return $"Patent: {Title} by {Authors}, published on {DatePublished.ToShortDateString()}, expires on {ExpirationDate.ToShortDateString()}, UniqueID: {UniqueID.ToString()}";
        }

    }
}
