using System;
using System.Linq;
using System.Text.RegularExpressions;
using SqlFormatter.Core.Interfaces;

namespace SqlFormatter.Core.Strategies
{
    public class SqlTokenizer : ITokenizer
    {
        public object Tokenize(object input)
        {
            var content = input as string;

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new Exception("Empty file");
            }

            var tokens = Regex.Split(content, @"([\s,\(\);])");

            tokens = tokens.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();

            return tokens;
        }
    }
}