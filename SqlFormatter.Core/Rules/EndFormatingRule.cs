﻿using System.Collections.Generic;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Rules
{
    internal class EndFormatingRule : FormatingRule
    {
        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            Before = IndentLine($"{Constants.NewLine}", indentationIncremental);

            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}