using System;
using System.Collections.Generic;
using Autofac.Features.Indexed;
using SqlFormatter.Core.Interfaces;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Strategies
{
    public class SqlFormattorProcessor : IFormattor
    {
        private readonly IFormattor _formattor;
        private readonly ITokenIdentifier _tokenIdentifier;
        private readonly ITokenImprover _tokenImprover;
        private readonly ITokenizer _tokenizer;

        public SqlFormattorProcessor(
            ITokenizer tokenizer, 
            ITokenIdentifier tokenIdentifier, 
            IIndex<string,IFormattor> formattor,
            ITokenImprover tokenImprover)
        {
            _tokenizer = tokenizer;
            _tokenIdentifier = tokenIdentifier;
            _formattor = formattor["SqlFormattor"];
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