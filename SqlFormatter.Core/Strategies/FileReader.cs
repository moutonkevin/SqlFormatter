using System.IO;
using SqlFormatter.Core.Interfaces;

namespace SqlFormatter.Core.Strategies
{
    public class FileReader : IReader
    {
        public string GetAll(object key)
        {
            return File.ReadAllText((string) key);
        }
    }
}