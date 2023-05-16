using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        private readonly Func<string, bool> _filter;
        private readonly string _root;

        public FileSystemVisitor(string root, Func<string, bool> filter = null)
        {
            _root = root;
            _filter = filter ?? (s => true);
        }

        public IEnumerable<string> GetFileSystemEntries()
        {
            return GetFileSystemEntries(_root);
        }

        private IEnumerable<string> GetFileSystemEntries(string path)
        {
            foreach (var entry in Directory.GetFileSystemEntries(path))
            {
                if (_filter(entry))
                {
                    yield return entry;
                }

                if (Directory.Exists(entry))
                {
                    foreach (var subEntry in GetFileSystemEntries(entry))
                    {
                        yield return subEntry;
                    }
                }
            }
        }
    }
}
