using System.Collections.Generic;
using System.Text;
using SqlFormatter.Core.Interfaces;
using SqlFormatter.Core.Models;
using SqlFormatter.Core.Rules;

namespace SqlFormatter.Core.Strategies
{
    public class SqlFormattor : IFormattor
    {
        private readonly Dictionary<SqlTokenTypes, FormatingRule> _formatingRules = new Dictionary<SqlTokenTypes, FormatingRule>
        {
            {SqlTokenTypes.KeywordSelect, new SelectFormatingRule()},
            {SqlTokenTypes.KeywordFrom, new FromFormatingRule()},
            {SqlTokenTypes.KeywordWhere, new WhereFormatingRule()},
            {SqlTokenTypes.Star, new StarFormatingRule()},
            {SqlTokenTypes.Coma, new ComaFormatingRule()},
            {SqlTokenTypes.KeywordJoin, new JoinFormatingRule()},
            {SqlTokenTypes.KeywordInner, new InnerFormatingRule()},
            {SqlTokenTypes.KeywordOn, new OnFormatingRule()},
            {SqlTokenTypes.KeywordGroup, new GroupFormatingRule()},
            {SqlTokenTypes.KeywordBy, new ByFormatingRule()},
            {SqlTokenTypes.KeywordDeclare, new DeclareTypeFormatingRule()},
            {SqlTokenTypes.Column, new ColumnFormatingRule()},
            {SqlTokenTypes.Number, new NumberFormatingRule()},
            {SqlTokenTypes.LogicalOperator, new LogicalOperatorFormatingRule()},
            {SqlTokenTypes.KeywordGo, new GoFormatingRule()},
            {SqlTokenTypes.KeywordCreate, new CreateFormatingRule()},
            {SqlTokenTypes.VariableName, new VariableFormatingRule()},
            {SqlTokenTypes.KeywordAs, new AsFormatingRule()},
            {SqlTokenTypes.KeywordBegin, new BeginFormatingRule()},
            {SqlTokenTypes.KeywordEnd, new EndFormatingRule()},
            {SqlTokenTypes.ParenthesisClose, new ParenthesisCloseFormatingRule()},
            {SqlTokenTypes.ParenthesisOpen, new ParenthesisOpenFormatingRule()},
            {SqlTokenTypes.KeywordIf, new IfFormatingRule()},
            {SqlTokenTypes.IsolationLevel, new IsolationLevelFormatingRule()},
            {SqlTokenTypes.EndOfLine, new EndOfLineFormatingRule()},
            {SqlTokenTypes.KeywordInto, new IntoFormatingRule()},
            {SqlTokenTypes.KeywordInsert, new InsertFormatingRule()},
            {SqlTokenTypes.KeywordWhile, new WhileFormatingRule()},
            {SqlTokenTypes.KeywordUpdate, new UpdateFormatingRule()},
            {SqlTokenTypes.KeywordSet, new SetFormatingRule()},
            {SqlTokenTypes.KeywordCommitRollback, new CommitRollbackFormatingRule()},
            {SqlTokenTypes.KeywordElse, new ElseFormatingRule()},
            {SqlTokenTypes.KeywordOrder, new OrderByFormatingRule()},
            {SqlTokenTypes.KeywordValues, new ValuesFormatingRule()},
        };

        private FormatingRule GetRuleFromToken(Token token)
        {
            foreach (var rule in _formatingRules)
            {
                if (rule.Key == token.Type)
                {
                    return rule.Value;
                }
            }
            return new DefaultFormatingRule();
        }

        private void AdjectIndentationIncrementPre(Token token, ref int indentationIncrement)
        {
            if (token.Type == SqlTokenTypes.KeywordEnd ||
                token.Type == SqlTokenTypes.ParenthesisClose
                )
            {
                indentationIncrement--;
            }
        }

        private void AdjectIndentationIncrementPost(Token token, ref int indentationIncrement)
        {
            if (token.Type == SqlTokenTypes.KeywordBegin ||
                token.Type == SqlTokenTypes.KeywordIf ||
                token.Type == SqlTokenTypes.ParenthesisOpen
                )
            {
                indentationIncrement++;
            }
        }

        public string Format(object value)
        {
            var identifiedTokens = value as List<Token>;
            var resultBuilder = new StringBuilder();
            var indentationIncrement = 0;

            for (var counter = 0; counter < identifiedTokens.Count; counter++)
            {
                var currentToken = identifiedTokens[counter];
                var rule = GetRuleFromToken(currentToken);

                AdjectIndentationIncrementPre(currentToken, ref indentationIncrement);

                var currentFormat = rule.GetFormat(counter, identifiedTokens, indentationIncrement);

                resultBuilder.Append(currentFormat);
                AdjectIndentationIncrementPost(currentToken, ref indentationIncrement);
            }

            return resultBuilder.ToString();
        }
    }
}
