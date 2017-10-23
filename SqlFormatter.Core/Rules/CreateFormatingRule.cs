using System.Collections.Generic;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Rules
{
    class CreateFormatingRule : FormatingRule
    {
        public CreateFormatingRule()
        {
            After = Constants.SmallIndentation;
        }

        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            Before = IndentLine(Constants.NewLine, indentationIncremental);

            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}
