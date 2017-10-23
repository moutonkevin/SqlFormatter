using System.Collections.Generic;
using SqlFormatter.Core.Models;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Rules
{
    public class AsFormatingRule : FormatingRule
    {
        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            Before = IndentLine(Constants.NewLine, indentationIncremental);
            After = Constants.SmallIndentation;

            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);
            var previousToken = GetTokenFromPosition(tokens, currentTokenPosition - 1);

            if (previousToken?.Type == SqlTokenTypes.VariableName ||
                previousToken?.Type == SqlTokenTypes.TableName)
            {
                Before = IndentLine(Constants.NoIndentation, indentationIncremental);
            }

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}
