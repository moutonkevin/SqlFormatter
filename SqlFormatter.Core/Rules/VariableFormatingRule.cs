using System.Collections.Generic;
using SqlFormatter.Core.Models;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Rules
{
    public class VariableFormatingRule : FormatingRule
    {
        public VariableFormatingRule()
        {
            After = Constants.SmallIndentation;
        }

        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            Before = IndentLine(Constants.NewLine, indentationIncremental);

            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);
            var previousToken = GetTokenFromPosition(tokens, currentTokenPosition - 1);
            var previousPreviousToken = GetTokenFromPosition(tokens, currentTokenPosition - 2);

            if (previousPreviousToken?.Type == SqlTokenTypes.KeywordProcedure)
                Before = IndentLine($"{Constants.NewLine}{Constants.MediumIndentation}", indentationIncremental);
            else if (previousToken?.Type == SqlTokenTypes.Coma ||
                     previousToken?.Type == SqlTokenTypes.KeywordFrom ||
                     previousToken?.Type == SqlTokenTypes.KeywordJoin ||
                     previousToken?.Type == SqlTokenTypes.KeywordDeclare ||
                     previousToken?.Type == SqlTokenTypes.MathematicalOperators ||
                     previousToken?.Type == SqlTokenTypes.KeywordWhile ||
                     previousToken?.Type == SqlTokenTypes.KeywordIf)
                Before = IndentLine(Constants.NoIndentation, indentationIncremental);
            else if (previousToken?.Type == SqlTokenTypes.KeywordSelect)
                Before = IndentLine($"{Constants.NoIndentation}", indentationIncremental);

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}