using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        private readonly Func<string, bool> _filter;
        private readonly string _root;
        private readonly Func<string, bool> _exclude;

        public FileSystemVisitor(string root, Func<string, bool> filter = null, Func<string, bool> exclude = null)
        {
            _root = root;
            _filter = filter ?? (s => true);
            _exclude = exclude ?? (s => false);
        }

        public IEnumerable<string> Traverse()
        {
            OnStart(_root);
            foreach (var file in TraverseDirectory(_root))
            {
                if (_filter(file))
                {
                    OnFilteredFileFound(file);
                    yield return file;
                }
                else
                {
                    OnFileFound(file);
                }
            }
            OnFinish(_root);
        }

        private IEnumerable<string> TraverseDirectory(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                yield return file;
            }

            foreach (var subDirectory in Directory.GetDirectories(directory))
            {
                OnDirectoryFound(subDirectory);
                foreach (var file in TraverseDirectory(subDirectory))
                {
                    yield return file;
                }
            }
        }

        public FileSystemVisitor Exclude(Func<string, bool> predicate)
        {
            return new FileSystemVisitor(_root, _filter, s => _exclude(s) || predicate(s));
        }

        public event Action<string> FileFound = delegate { };
        public event Action<string> DirectoryFound = delegate { };
        public event Action<string> FilteredFileFound = delegate { };
        public event Action<string> FilteredDirectoryFound = delegate { };
        public event Action<string> Start = delegate { };
        public event Action<string> Finish = delegate { };

        protected virtual void OnFileFound(string path)
        {
            FileFound(path);
        }

        protected virtual void OnDirectoryFound(string path)
        {
            DirectoryFound(path);
        }

        protected virtual void OnFilteredFileFound(string path)
        {
            FilteredFileFound(path);
        }

        protected virtual void OnFilteredDirectoryFound(string path)
        {
            FilteredDirectoryFound(path);
        }

        protected virtual void OnStart(string path)
        {
            Start(path);
        }

        protected virtual void OnFinish(string path)
        {
            Finish(path);
        }
    }
}

