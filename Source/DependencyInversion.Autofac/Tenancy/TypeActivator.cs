/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Autofac;
using Autofac.Core.Resolving;

namespace Dolittle.DependencyInversion.Autofac.Tenancy
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeActivator"/>
    /// </summary>
    public class TypeActivator : ITypeActivator
    {
        global::Autofac.IContainer _container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        public TypeActivator(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterBuildCallback(c => _container = c);
        }


        /// <inheritdoc/>
        public object CreateInstanceFor(IComponentContext context, Type service, Type type)
        {
            object instance;
            var constructors = type.GetConstructors().ToArray();
            if (constructors.Length > 1) throw new Exception($"Unable to create instance of '{type.AssemblyQualifiedName}' - more than one constructor");
            var constructor = constructors[0];
            var parameterInstances = constructor.GetParameters().Select(_ => _container.Resolve(_.ParameterType)).ToArray();

            var instanceLookup = context as IInstanceLookup;

            if( service.ContainsGenericParameters ) 
            {
                var genericArguments = instanceLookup.ComponentRegistration.Activator.LimitType.GetGenericArguments();
                var targetType = type.MakeGenericType(genericArguments);
                instance = Activator.CreateInstance(targetType, parameterInstances);
            } 
            else 
            {
                instance = Activator.CreateInstance(type, parameterInstances);
            }
            
            return instance;
        }
        

    }
}