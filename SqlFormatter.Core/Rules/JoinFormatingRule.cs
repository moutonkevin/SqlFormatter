using System.Collections.Generic;
using SqlFormatter.Core.Models;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Rules
{
    public class JoinFormatingRule : FormatingRule
    {
        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);
            var previousToken = GetTokenFromPosition(tokens, currentTokenPosition - 1);

            if (previousToken?.Type == SqlTokenTypes.KeywordInner)
                Before = Constants.NoIndentation;
            else
                Before = IndentLine(Constants.NewLine, indentationIncremental);

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}