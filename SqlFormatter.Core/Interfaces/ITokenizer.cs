using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFormatter.Core.Interfaces
{
    public interface ITokenizer
    {
        object Tokenize(object input);
    }
}
