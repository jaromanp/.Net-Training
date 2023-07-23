using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Fundamentals.Interface
{
    interface ICacheService
    {
        void AddToCache(string key, IDocument document, TimeSpan duration);
        IDocument GetFromCache(string key);
    }
}
