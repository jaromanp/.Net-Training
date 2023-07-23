using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.DTO
{
    public class LocalizedBook : BaseBook
    {
        public string OriginalPublisher { get; set; }
        public string CountryOfLocalization { get; set; }
        public string LocalPublisher { get; set; }

        public LocalizedBook(string isbn, string title, int numberOfPages, string authors, string originalPublisher, string countryOfLocalization, string localPublisher, DateTime datePublished)
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
            else
            {
                Authors = authors;
            }
            if (numberOfPages < 0)
            {
                throw new ArgumentException("The number of pages cannot be zero or negative.");
            }
            NumberOfPages = numberOfPages;
            OriginalPublisher = originalPublisher;
            CountryOfLocalization = countryOfLocalization;
            LocalPublisher = localPublisher;
            DatePublished = datePublished;
            Type = DocumentType.LocalizedBook;
        }

        public override string GetCardInfo()
        {
            return $"Localized Book: {Title} by {Authors}, published by {LocalPublisher} in {CountryOfLocalization} on {DatePublished.ToShortDateString()}";
        }
    }
}
