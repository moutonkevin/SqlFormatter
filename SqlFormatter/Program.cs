using System;
using Autofac;
using SqlFormatter.Core.Interfaces;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter
{
    internal class Program
    {
        private static IContainer Container { get; set; }

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

            Container = builder.Build();
        }

        private static void Main(string[] args)
        {
            Register();

            var inputFilePath = @"C:\Users\kevin.mouton\Desktop\test.sql";
            var outputFilePath = @"C:\Users\kevin.mouton\Desktop\test_result.sql";

            using (var scope = Container.BeginLifetimeScope())
            {
                var formattor = scope.ResolveKeyed<IFormattor>("SqlFormattorProcessor");
                var reader = scope.Resolve<IReader>();
                var writer = scope.Resolve<IWriter>();

                var fileContent = reader.GetAll(inputFilePath);
                var formattedContent = formattor.Format(fileContent);
                writer.Save(formattedContent, outputFilePath);
            }

            Console.ReadKey();
        }
    }
}