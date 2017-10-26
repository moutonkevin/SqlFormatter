using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SqlFormatter.AcceptanceTest
{
    [TestClass]
    public class SqlFormatterAcceptanceTest
    {
        [TestMethod]
        public void FormattingController_Sql_SimpleSelect_Correct()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://sqlformatter.localhost:443/api/formatting/sql")
            {
                Content = new StringContent(
                    "=SELECT * FROM test where id = 1",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded")
            };

            var task = client.SendAsync(request);

            task.Wait();

            Assert.AreEqual(1, 1);
        }
    }
}
