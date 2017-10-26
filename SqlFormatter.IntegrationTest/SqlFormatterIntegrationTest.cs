using System.IO;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlFormatter.Core.Interfaces;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.IntegrationTest
{
    [TestClass]
    public class SqlFormatterIntegrationTest
    {
        private static IContainer _container { get; set; }

        private static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileReader>().As<IReader>();
            builder.RegisterType<FileWriter>().As<IWriter>();
            builder.RegisterType<SqlTokenizer>().As<ITokenizer>();
            builder.RegisterType<SqlTokenIdentifier>().As<ITokenIdentifier>();
            builder.RegisterType<SqlTokenImprover>().As<ITokenImprover>();
            builder.RegisterType<SqlFormattor>().Keyed<IFormattor>("SqlFormattor");
            builder.RegisterType<SqlFormattorProcessor>().Keyed<IFormattor>("SqlFormattorProcessor");

            _container = builder.Build();
        }

        [TestInitialize]
        public void Init()
        {
            Register();
        }

        public void CompareOutputs(string testFolderPath, string testPath, string resultTestPath)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var formattor = scope.ResolveKeyed<IFormattor>("SqlFormattorProcessor");
                var reader = scope.Resolve<IReader>();

                var testFilePath = Path.Combine(testFolderPath, testPath);
                var testFileContent = reader.GetAll(testFilePath);
                var formattedContent = formattor.Format(testFileContent);

                var resultTestFilePath = Path.Combine(testFolderPath, resultTestPath);
                var resultTestFileContent = reader.GetAll(resultTestFilePath).Replace("\\n", "\n");

                Assert.AreEqual(formattedContent, resultTestFileContent);
            }
        }

        [TestMethod]
        public void SqlFormatter_SimpleSelect_Correct()
        {
            var sqlQueriesFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "SqlQueries");

            CompareOutputs(sqlQueriesFolderPath, "SimpleSelect/SimpleSelect.sql", "SimpleSelect/SimpleSelect_result.sql");
        }

        [TestMethod]
        public void SqlFormatter_ComplexSelect_Correct()
        {
            var sqlQueriesFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "SqlQueries");

            CompareOutputs(sqlQueriesFolderPath, "ComplexSelect/ComplexSelect.sql", "ComplexSelect/ComplexSelect_result.sql");
        }
    }
}
