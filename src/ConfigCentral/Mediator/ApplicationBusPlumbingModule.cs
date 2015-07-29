using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace ConfigCentral.Mediator
{
    public class ApplicationBusPlumbingModule : Module
    {
        private readonly List<Type> _handlerTypes = new List<Type>();

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacApplicationBus>()
                .As<IApplicationBus>()
                .SingleInstance();

            builder.RegisterGeneric(typeof (NullHandler<,>))
                .As(typeof (IRequestHandler<,>));

            builder.RegisterTypes(_handlerTypes.Distinct()
                .ToArray())
                .AsImplementedInterfaces();
        }

        public ApplicationBusPlumbingModule RegisterHandlerTypesIn(params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetTypes()
                .Where(t => !t.IsAbstract && t.IsClosedTypeOf(typeof (IRequestHandler<,>))));
            _handlerTypes.AddRange(types);
            return this;
        }
    }
}