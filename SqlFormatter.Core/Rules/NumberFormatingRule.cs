using System.Collections.Generic;
using SqlFormatter.Core.Models;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Rules
{
    public class NumberFormatingRule : FormatingRule
    {
        public NumberFormatingRule()
        {
            Before = Constants.NoIndentation;
            After = Constants.SmallIndentation;
        }

        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);
            var nextToken = GetTokenFromPosition(tokens, currentTokenPosition + 1);
            var previousToken = GetTokenFromPosition(tokens, currentTokenPosition - 1);

            if (nextToken?.Type == SqlTokenTypes.Column &&
                previousToken?.Type == SqlTokenTypes.KeywordTop)
            {
                Before = Constants.NoIndentation;
                After = IndentLine($"{Constants.NewLine}{Constants.MediumIndentation}", indentationIncremental);
            }
            else if (nextToken?.Type == SqlTokenTypes.ParenthesisClose)
            {
                After = Constants.NoIndentation;
            }

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}