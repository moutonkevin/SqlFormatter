using System.Collections.Generic;
using SqlFormatter.Core.Models;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Rules
{
    public class SelectFormatingRule : FormatingRule
    {
        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            Before = IndentLine($"{Constants.NewLine}{Constants.NewLine}", indentationIncremental);
            After = IndentLine($"{Constants.NewLine}{Constants.MediumIndentation}", indentationIncremental);

            var nextToken = GetTokenFromPosition(tokens, currentTokenPosition + 1);
            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);
            var previousToken = GetTokenFromPosition(tokens, currentTokenPosition - 1);

            if (nextToken?.Type == SqlTokenTypes.KeywordTop ||
                nextToken?.Type == SqlTokenTypes.Star)
            {
                After = Constants.SmallIndentation;

                return $"{Before}{currentToken.Value}{After}";
            }

            if (previousToken?.Type == SqlTokenTypes.KeywordBegin)
            {
                Before = IndentLine($"{Constants.NewLine}", indentationIncremental);

                return $"{Before}{currentToken.Value}{After}";
            }
            return $"{Before}{currentToken.Value}{After}";
        }
    }
}