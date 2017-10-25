using System.Collections.Generic;
using SqlFormatter.Core.Models;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Rules
{
    public class StarFormatingRule : FormatingRule
    {
        public StarFormatingRule()
        {
            Before = Constants.SmallIndentation;
            After = Constants.SmallIndentation;
        }

        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);
            var nextToken = GetTokenFromPosition(tokens, currentTokenPosition + 1);
            var previousToken = GetTokenFromPosition(tokens, currentTokenPosition - 1);

            if (nextToken?.Type == SqlTokenTypes.Coma)
                Before = IndentLine($"{Constants.NewLine}{Constants.MediumIndentation}", indentationIncremental);

            if (previousToken?.Type == SqlTokenTypes.Coma)
                Before = Constants.NoIndentation;

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}