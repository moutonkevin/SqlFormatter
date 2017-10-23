using System;
using SqlFormatter.Core.Models;
using System.Collections.Generic;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Rules
{
    public class ParenthesisOpenFormatingRule : FormatingRule
    {
        public override string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            Before = Constants.SmallIndentation;
            After = Constants.NoIndentation;

            var currentToken = GetTokenFromPosition(tokens, currentTokenPosition);
            var previousToken = GetTokenFromPosition(tokens, currentTokenPosition - 1);

            if (previousToken?.Type == SqlTokenTypes.KeywordIf)
            {
                Before = Constants.NoIndentation;
                After = Constants.NoIndentation;
            }
            else if (previousToken?.Type == SqlTokenTypes.TableName)
            {
                Before = IndentLine($"{Constants.NewLine}{Constants.MediumIndentation}", indentationIncremental);
                After = Constants.NoIndentation;
            }
            else if (previousToken?.Type == SqlTokenTypes.KeywordWhere ||
                previousToken?.Type == SqlTokenTypes.LogicalOperator)
            {
                Console.WriteLine($"ParenthesisOpenFormatingRule: special indentation for Previous[{previousToken.Type} {previousToken.Value}] Current[{currentToken.Type} {currentToken.Value}]");

                Before = IndentLine($"{Constants.NewLine}{Constants.MediumIndentation}", indentationIncremental);
                After = IndentLine($"{Constants.NewLine}{Constants.MediumIndentation}{Constants.MediumIndentation}", indentationIncremental);
            }

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}
