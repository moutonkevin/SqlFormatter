using System;
using System.Collections.Generic;
using SqlFormatter.Core.Interfaces;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Strategies
{
    public class SqlFormattorProcessor : IFormattor
    {
        private readonly ITokenizer _tokenizer;
        private readonly ITokenIdentifier _tokenIdentifier;
        private readonly IFormattor _formattor;
        private readonly ITokenImprover _tokenImprover;

        public SqlFormattorProcessor(ITokenizer tokenizer, ITokenIdentifier tokenIdentifier, IFormattor formattor, ITokenImprover tokenImprover)
        {
            _tokenizer = tokenizer;
            _tokenIdentifier = tokenIdentifier;
            _formattor = formattor;
            _tokenImprover = tokenImprover;
        }

        public string Format(object value)
        {
            Console.WriteLine($"Formating: {value}");

            var tokens = _tokenizer.Tokenize(value);

            var identifiedTokens = _tokenIdentifier.IdentifyTokens(tokens);

            (identifiedTokens as List<Token>).ForEach(token => _tokenImprover.Improve(token));

            var result = _formattor.Format(identifiedTokens);

            Console.WriteLine("------------------");
            Console.WriteLine(result);

            return result;
        }
    }
}
