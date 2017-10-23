using System;
using SqlFormatter.Core.Strategies;
using SqlFormatter.Core.Interfaces;

namespace SqlFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            IReader reader = new FileReader();
            IWriter writer = new FileWriter();
            ITokenizer tokenizer = new SqlTokenizer();
            ITokenIdentifier tokenIdentifier = new SqlTokenIdentifier();
            ITokenImprover tokenImprover = new SqlTokenImprover();
            IFormattor formattor = new SqlFormattor();
            IFormattor formattorProcessor = new SqlFormattorProcessor(tokenizer, tokenIdentifier, formattor, tokenImprover);

            var inputFilePath = @"C:\Users\kevin.mouton\Desktop\test.sql";
            var outputFilePath = @"C:\Users\kevin.mouton\Desktop\test_result.sql";

            var fileContent  = reader.GetAll(inputFilePath);
            var formattedContent = formattorProcessor.Format(fileContent);
            writer.Save(formattedContent, outputFilePath);

            Console.ReadKey();
        }
    }
}
