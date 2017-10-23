using System.Collections.Generic;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Rules
{
    class EndOfLineFormatingRule : FormatingRule
    {
        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);

            After = IndentLine($"{Constants.NewLine}", indentationIncremental);

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}
