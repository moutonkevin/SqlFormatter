using System.Collections.Generic;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Rules
{
    class InsertFormatingRule : FormatingRule
    {
        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            Before = IndentLine($"{Constants.NewLine}{Constants.NewLine}", indentationIncremental);
            After = Constants.SmallIndentation;

            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}
