using SqlFormatter.Core.Interfaces;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Strategies
{
    public class SqlTokenImprover : ITokenImprover
    {
        public void Improve<TInput>(TInput token)
        {
            var sqlToken = token as Token;

            if (sqlToken.Type == SqlTokenTypes.KeywordSelect ||
                sqlToken.Type == SqlTokenTypes.KeywordDeclare ||
                sqlToken.Type == SqlTokenTypes.KeywordBy ||
                sqlToken.Type == SqlTokenTypes.KeywordGroup ||
                sqlToken.Type == SqlTokenTypes.KeywordOrder ||
                sqlToken.Type == SqlTokenTypes.KeywordTop ||
                sqlToken.Type == SqlTokenTypes.KeywordAs ||
                sqlToken.Type == SqlTokenTypes.KeywordFrom ||
                sqlToken.Type == SqlTokenTypes.KeywordInner ||
                sqlToken.Type == SqlTokenTypes.KeywordJoin ||
                sqlToken.Type == SqlTokenTypes.KeywordOn ||
                sqlToken.Type == SqlTokenTypes.KeywordWhere ||
                sqlToken.Type == SqlTokenTypes.KeywordInsert ||
                sqlToken.Type == SqlTokenTypes.KeywordUpdate ||
                sqlToken.Type == SqlTokenTypes.KeywordIf ||
                sqlToken.Type == SqlTokenTypes.KeywordWhere ||
                sqlToken.Type == SqlTokenTypes.KeywordBegin ||
                sqlToken.Type == SqlTokenTypes.KeywordEnd ||
                sqlToken.Type == SqlTokenTypes.KeywordInto ||
                sqlToken.Type == SqlTokenTypes.KeywordElse ||
                sqlToken.Type == SqlTokenTypes.MathematicalOperators ||
                sqlToken.Type == SqlTokenTypes.KeywordSet ||
                sqlToken.Type == SqlTokenTypes.KeywordValues ||
                sqlToken.Type == SqlTokenTypes.DataType)
                sqlToken.Value = sqlToken.Value.ToUpperInvariant();
        }
    }
}