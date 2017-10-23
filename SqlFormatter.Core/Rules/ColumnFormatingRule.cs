using System.Collections.Generic;
using SqlFormatter.Core.Models;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Rules
{
    public class ColumnFormatingRule : FormatingRule
    {
        public ColumnFormatingRule()
        {
            Before = Constants.NoIndentation;
        }

        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);
            var nextToken = GetTokenFromPosition(tokens, currentTokenPosition + 1);

            if (nextToken?.Type == SqlTokenTypes.Coma)
            {
                After = Constants.NoIndentation;
            }

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}