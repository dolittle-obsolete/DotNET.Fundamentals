// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Autofac;
using Autofac.Core.Resolving;

namespace Dolittle.DependencyInversion.Autofac.Tenancy
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeActivator"/>.
    /// </summary>
    public class TypeActivator : ITypeActivator
    {
        global::Autofac.IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeActivator"/> class.
        /// </summary>
        /// <param name="containerBuilder"><see cref="ContainerBuilder"/> instance.</param>
        public TypeActivator(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterBuildCallback(c => _container = c);
        }

        /// <inheritdoc/>
        public object CreateInstanceFor(IComponentContext context, Type service, Type type)
        {
            var constructors = type.GetConstructors().ToArray();
            if (constructors.Length > 1) throw new AmbiguousConstructor(type);
            var constructor = constructors[0];
            var parameterInstances = constructor.GetParameters().Select(_ => _container.Resolve(_.ParameterType)).ToArray();

            var instanceLookup = context as IInstanceLookup;

            if (service.ContainsGenericParameters)
            {
                var genericArguments = instanceLookup.ComponentRegistration.Activator.LimitType.GetGenericArguments();
                var targetType = type.MakeGenericType(genericArguments);
                return Activator.CreateInstance(targetType, parameterInstances);
            }
            else
            {
                return Activator.CreateInstance(type, parameterInstances);
            }
        }
    }
}