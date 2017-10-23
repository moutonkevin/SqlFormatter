using System.Collections.Generic;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Rules
{
    public class ComaFormatingRule : FormatingRule
    {
        public ComaFormatingRule()
        {
            Before = Constants.NoIndentation;
        }

        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            After = IndentLine($"{Constants.NewLine}{Constants.MediumIndentation}", indentationIncremental);

            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}
