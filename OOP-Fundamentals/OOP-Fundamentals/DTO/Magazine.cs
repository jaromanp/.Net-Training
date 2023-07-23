using OOP_Fundamentals.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.DTO
{
    public class Magazine : IDocument
    {
        public string Title { get; set; }
        public string Publisher { get; set; }

        public int ReleaseNumber { get; set; }
        public DateTime DatePublished { get; set; }
        public DocumentType Type { get; set; }

        public Magazine(string title, string publisher, int releaseNumber, DateTime datePublished) {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("The title is required.");
            Title = title;
            if (string.IsNullOrWhiteSpace(publisher))
                throw new ArgumentException("The publisher is required.");
            Publisher = publisher;            
            ReleaseNumber = releaseNumber;
            DatePublished = datePublished;
            Type = DocumentType.Magazine;
        }

        public string GetCardInfo()
        {
            return $"Magazine: {Title}, published by {Publisher}, release number {ReleaseNumber}, published on {DatePublished.ToShortDateString()}";
        }
    }
}
