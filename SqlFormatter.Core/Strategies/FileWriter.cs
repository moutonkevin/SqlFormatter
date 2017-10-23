using System.IO;
using SqlFormatter.Core.Interfaces;

namespace SqlFormatter.Core.Strategies
{
    public class FileWriter : IWriter
    {
        public void Save(object value, object destination)
        {
            File.Open((string)destination, FileMode.Create, FileAccess.Write);
        }
    }
}
