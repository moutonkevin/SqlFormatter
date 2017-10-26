using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlFormatter.Core.Interfaces;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.UnitTest
{
    [TestClass]
    public class SqlTokenizerTests
    {
        private readonly ITokenizer _tokenizer;

        public SqlTokenizerTests()
        {
            _tokenizer = new SqlTokenizer();
        }

        public bool IsExceptionHappening(Func<object, object> toExecute, string input)
        {
            try
            {
                toExecute(input);
            }
            catch (Exception e)
            {
                return true;
            }
            return false;
        }

        [TestMethod]
        public void Tokenize_EmptyInput_ThrowsException()
        {
            var hasExceptionHappened1 = IsExceptionHappening(_tokenizer.Tokenize, string.Empty);
            var hasExceptionHappened2 = IsExceptionHappening(_tokenizer.Tokenize, null);

            Assert.IsTrue(hasExceptionHappened1);
            Assert.IsTrue(hasExceptionHappened2);
        }

        [TestMethod]
        public void Tokenize_OnlyComasInput_3Tokens()
        {
            var tokens = _tokenizer.Tokenize(",,,") as string[];

            Assert.IsTrue(tokens.Length == 3);
        }

        [TestMethod]
        public void Tokenize_OnlyOpenParenthesisInput_2Tokens()
        {
            var tokens = _tokenizer.Tokenize("((") as string[];

            Assert.IsTrue(tokens.Length == 2);
        }

        [TestMethod]
        public void Tokenize_OnlyCloseParenthesisInput_4Tokens()
        {
            var tokens = _tokenizer.Tokenize("))))") as string[];

            Assert.IsTrue(tokens.Length == 4);
        }

        [TestMethod]
        public void Tokenize_OnlySemiColonInput_1Tokens()
        {
            var tokens = _tokenizer.Tokenize(";") as string[];

            Assert.IsTrue(tokens.Length == 1);
        }
    }
}
