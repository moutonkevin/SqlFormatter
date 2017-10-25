using System.Collections.Generic;
using SqlFormatter.Core.Models;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Rules
{
    internal class ParenthesisCloseFormatingRule : FormatingRule
    {
        public ParenthesisCloseFormatingRule()
        {
            After = Constants.SmallIndentation;
        }

        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            Before = IndentLine($"{Constants.NewLine}{Constants.MediumIndentation}", indentationIncremental);

            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);
            var previousToken = GetTokenFromPosition(tokens, currentTokenPosition - 1);
            var previouspreviouspreviousToken = GetTokenFromPosition(tokens, currentTokenPosition - 3);
            var nextToken = GetTokenFromPosition(tokens, currentTokenPosition + 1);

            if (previouspreviouspreviousToken?.Type == SqlTokenTypes.DataType ||
                nextToken?.Type == SqlTokenTypes.KeywordBegin ||
                previousToken?.Type == SqlTokenTypes.IsolationLevel ||
                previousToken?.Type == SqlTokenTypes.ParenthesisOpen)
            {
                Before = Constants.NoIndentation;
                After = Constants.NoIndentation;
            }

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}