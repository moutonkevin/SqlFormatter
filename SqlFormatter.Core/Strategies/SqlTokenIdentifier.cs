using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SqlFormatter.Core.Interfaces;
using SqlFormatter.Core.Models;

namespace SqlFormatter.Core.Strategies
{
    public enum SqlTokenTypes
    {
        KeywordSelect,
        KeywordFrom,
        KeywordInner,
        KeywordJoin,
        KeywordWhere,
        KeywordTop,
        KeywordAs,
        KeywordOn,
        KeywordOrder,
        KeywordGroup,
        KeywordBy,
        KeywordDeclare,
        KeywordUse,
        KeywordCreate,
        KeywordProcedure,
        KeywordBegin,
        KeywordEnd,
        KeywordGo,
        KeywordInto,
        VariableName,
        KeywordIf,
        KeywordElse,
        KeywordWhile,
        KeywordInsert,
        KeywordUpdate,
        KeywordSet,
        KeywordValues,
        KeywordCommitRollback,
        DataType,
        VariableDefaultValue,
        StoredProcedureName,
        TableName,
        Function,
        Alias,
        Number,
        Star,
        Coma,
        Column,
        EndOfLine,
        LogicalOperator,
        MathematicalOperators,
        ParenthesisOpen,
        ParenthesisClose,
        IsolationLevel,
        Unknown
    }

    //http://regexstorm.net/tester
    //https://msdn.microsoft.com/en-us/library/ae5bf541(v=vs.100).aspx

    public class SqlTokenIdentifier : ITokenIdentifier
    {
        private readonly Dictionary<Regex, SqlTokenTypes> _tokenTypes = new Dictionary<Regex, SqlTokenTypes>
        {
            { new Regex(@"^VALUES", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordValues } ,
            { new Regex(@"^SET", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordSet } ,
            { new Regex(@"^INSERT", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordInsert } ,
            { new Regex(@"^UPDATE", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordUpdate } ,
            { new Regex(@"^WHILE", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordWhile } ,
            { new Regex(@"^INTO", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordInto } ,
            { new Regex(@"^IF", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordIf } ,
            { new Regex(@"^ELSE", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordElse } ,
            { new Regex(@"^GO", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordGo } ,
            { new Regex(@"^USE", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordUse } ,
            { new Regex(@"^CREATE", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordCreate } ,
            { new Regex(@"^(PROCEDURE|PROC)", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordProcedure } ,
            { new Regex(@"^BEGIN", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordBegin } ,
            { new Regex(@"^END", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordEnd } ,
            { new Regex(@"^SELECT", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordSelect } ,
            { new Regex(@"^FROM", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordFrom } ,
            { new Regex(@"^WHERE", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordWhere } ,
            { new Regex(@"^TOP", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordTop } ,
            { new Regex(@"^AS", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordAs } ,
            { new Regex(@"^ON", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordOn } ,
            { new Regex(@"^ORDER", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordOrder } ,
            { new Regex(@"^BY", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordBy } ,
            { new Regex(@"^GROUP", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordGroup } ,
            { new Regex(@"^INNER", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordInner } ,
            { new Regex(@"^JOIN", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordJoin } ,
            { new Regex(@"^DECLARE", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordDeclare } ,
            { new Regex(@"(ROWLOCK|NOLOCK)", RegexOptions.IgnoreCase), SqlTokenTypes.IsolationLevel } ,
            { new Regex(@"(COMMIT|ROLLBACK)", RegexOptions.IgnoreCase), SqlTokenTypes.KeywordCommitRollback } ,
            { new Regex(@"^;", RegexOptions.IgnoreCase), SqlTokenTypes.EndOfLine } ,
            { new Regex(@"^\d*$", RegexOptions.IgnoreCase), SqlTokenTypes.Number } ,
            { new Regex(@"[@][a-z0-9A-Z]*", RegexOptions.IgnoreCase), SqlTokenTypes.VariableName } ,
            { new Regex(@"\*", RegexOptions.IgnoreCase), SqlTokenTypes.Star } ,
            { new Regex(@",", RegexOptions.IgnoreCase), SqlTokenTypes.Coma },
            { new Regex(@"^(XML|BIT|INT|VARCHAR\(\d*|MAX\)|VARCHAR|NVARCHAR)", RegexOptions.IgnoreCase), SqlTokenTypes.DataType },
            { new Regex(@"\(", RegexOptions.IgnoreCase), SqlTokenTypes.ParenthesisOpen } ,
            { new Regex(@"\)", RegexOptions.IgnoreCase), SqlTokenTypes.ParenthesisClose } ,
            { new Regex(@"^(AND|OR)", RegexOptions.IgnoreCase), SqlTokenTypes.LogicalOperator },
            { new Regex(@"^(\+|\-|\*|\/|\=|\<|\>)", RegexOptions.IgnoreCase), SqlTokenTypes.MathematicalOperators }
        };

        private SqlTokenTypes GetTokenTypeFromValue(string tokenValue)
        {
            foreach (var tokenType in _tokenTypes)
            {
                if (tokenType.Key.IsMatch(tokenValue))
                {
                    Console.WriteLine($"Its a match! SqlTokenTypes:[{tokenType.Value}] Value:[{tokenValue}]");

                    return tokenType.Value;
                }
            }

            Console.WriteLine($"Unknown SqlTokenTypes:[{SqlTokenTypes.Unknown}] Value:[{tokenValue}]");

            return SqlTokenTypes.Unknown;
        }

        public object IdentifyTokens(object tokens)
        {
            var tokenArray = (string[]) tokens;
            var identifiedTokens = new List<Token>();

            foreach (var token in tokenArray)
            {
                var tokenType = GetTokenTypeFromValue(token);

                identifiedTokens.Add(new Token() {Value = token, Type = tokenType});
            }

            IdentifyUnknownTokens(identifiedTokens);

            return identifiedTokens;
        }

        private Token GetTokenFromPosition(List<Token> tokens, int relativePosition)
        {
            if (relativePosition >= tokens.Count ||
                relativePosition < 0)
            {
                return null;
            }

            return tokens[relativePosition];
        }

        private void IdentifyUnknownTokens(List<Token> tokens)
        {
            Console.WriteLine($"----------------- Second round -----------------");

            for (var counter = 0; counter < tokens.Count; counter++)
            {
                var currentToken = GetTokenFromPosition(tokens, counter);
                var nextToken = GetTokenFromPosition(tokens, counter + 1);
                var previousToken = GetTokenFromPosition(tokens, counter - 1);

                if (currentToken.Type == SqlTokenTypes.Unknown &&
                    (nextToken?.Type == SqlTokenTypes.Coma 
                    || nextToken?.Type == SqlTokenTypes.KeywordFrom))
                {
                    currentToken.Type = SqlTokenTypes.Column;

                    Console.WriteLine($"Its a match! SqlTokenTypes:[{currentToken.Type}] Value:[{currentToken.Value}]");
                }

                if (currentToken.Type == SqlTokenTypes.Unknown &&
                    (previousToken?.Type == SqlTokenTypes.KeywordFrom
                     || previousToken?.Type == SqlTokenTypes.KeywordJoin
                     || previousToken?.Type == SqlTokenTypes.KeywordInto))
                {
                    currentToken.Type = SqlTokenTypes.TableName;

                    Console.WriteLine($"Its a match! SqlTokenTypes:[{currentToken.Type}] Value:[{currentToken.Value}]");
                }
            }
        }
    }
}
