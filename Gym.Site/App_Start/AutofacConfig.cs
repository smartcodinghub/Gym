using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Gym.Infraestructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Gym.Site
{
    public class AutofacConfig
    {
        public static void Run()
        {
            SetAutofacWebAPI();
        }

        public static void SetAutofacWebAPI()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            MediatorRegister(builder);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            RepositoriesRegister(builder);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RepositoriesRegister(ContainerBuilder builder)
        {
            builder.RegisterType<GymDatabase>().AsSelf();

            builder.RegisterGeneric(typeof(Repository<>))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private static void MediatorRegister(ContainerBuilder builder)
        {
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => { object o; return c.TryResolve(t, out o) ? o : null; };
            }).InstancePerLifetimeScope();

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            }).InstancePerLifetimeScope();

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(GetAssemblyByName("Gym.Application"))
                .As(o => o.GetInterfaces()
                    .Where(i => i.IsClosedTypeOf(typeof(IAsyncRequestHandler<,>)))
                    .Select(i => new KeyedService("IAsyncRequestHandler", i))).InstancePerLifetimeScope().AsImplementedInterfaces();
        }

        public static Assembly GetAssemblyByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly => string.Equals(assembly.GetName().Name, name));
        }
    }
}