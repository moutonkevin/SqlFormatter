using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using SqlFormatter.Core.Interfaces;
using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Api
{
    public class Ioc
    {
        public static void RegisterAll()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterType<SqlTokenizer>()
                .As<ITokenizer>();

            builder.RegisterType<SqlTokenIdentifier>()
                .As<ITokenIdentifier>();

            builder.RegisterType<SqlTokenImprover>()
                .As<ITokenImprover>();

            builder.RegisterType<SqlFormattor>()
                .Keyed<IFormattor>("SqlFormattor");

            builder.RegisterType<SqlFormattorProcessor>()
                .Keyed<IFormattor>("SqlFormattorProcessor");

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiModelBinderProvider();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}