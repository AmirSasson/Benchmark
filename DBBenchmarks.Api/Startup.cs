using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Autofac;
using System.Reflection;

namespace StateMachineTests
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            IContainer container = InitializeDependencies();

            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }

        private IContainer InitializeDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(ElasticRepo<>)).As(typeof(IRepo<>)).SingleInstance();
            builder.RegisterType<ConsoleLogger>().As<ILogger>().SingleInstance();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            return builder.Build();
        }
    }
}
