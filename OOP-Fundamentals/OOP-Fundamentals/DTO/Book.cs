using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOP_Fundamentals.DTO
{
    public class Book : BaseBook
    {
        public string Publisher { get; set; }

        public Book(string isbn, string title, int numberOfPages, string authors, string publisher, DateTime datePublished)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("The ISBN is required.");
            ISBN = isbn;
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("The title is required.");
            Title = title;
            if (string.IsNullOrWhiteSpace(authors))
            {
                Authors = "Anonymous";
            }
            else {
                Authors = authors; 
            }
            if (numberOfPages < 0)
            {
                throw new ArgumentException("The number of pages cannot be zero or negative.");
            }
            NumberOfPages = numberOfPages;
            Publisher = publisher;
            DatePublished = datePublished;
            Type = DocumentType.Book;
        }

        public override string GetCardInfo()
        {
            return $"Book: {Title} by {Authors}, published by {Publisher} on {DatePublished.ToShortDateString()}";
        }
    }
}
