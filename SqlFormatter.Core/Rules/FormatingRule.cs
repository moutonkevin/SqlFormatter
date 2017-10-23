using System.Collections.Generic;
using System.Text;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Rules
{
    public abstract class FormatingRule
    {
        public string Before { get; set; }
        public string After { get; set; }

        protected FormatingRule()
        {
            Before = Constants.NoIndentation;
            After = Constants.SmallIndentation;
        }

        protected string IndentLine(string indentation, int indentationIncrement)
        {
            if (indentation.Contains(Constants.NewLine))
            {
                var indentationString = new StringBuilder();

                indentationString.Append(indentation);

                for (int i = 0; i < indentationIncrement; i++)
                {
                    indentationString.Append(Constants.MediumIndentation);
                }

                return indentationString.ToString();
            }

            return indentation;
        }

        protected Token GetTokenFromPosition(List<Token> tokens, int relativePosition)
        {
            if (relativePosition >= tokens.Count ||
                relativePosition < 0)
            {
                return null;
            }

            return tokens[relativePosition];
        }

        public virtual string GetFormat(int currentTokenPosition, List<Token> tokens, int indentationIncremental)
        {
            var currentToken = tokens[currentTokenPosition];

            return $"{Before}{currentToken.Value}{After}";
        }
    }
}
