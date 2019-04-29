/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace Dolittle.DependencyInversion.Autofac
{
    // Taken from https://stackoverflow.com/a/6994144

    /// <summary>
    /// Extension methods for 
    /// </summary>
    public static class ComponentContextExtensions
    {
        /// <summary>
        /// Resolves an unregistered service
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ResolveUnregistered(this IComponentContext context, Type serviceType, IEnumerable<Parameter> parameters)
        {
            var scope = context.Resolve<ILifetimeScope>();
            using (var innerScope = scope.BeginLifetimeScope(b => b.RegisterType(serviceType)))
            {
                IComponentRegistration reg;
                innerScope.ComponentRegistry.TryGetRegistration(new TypedService(serviceType), out reg);

                return context.ResolveComponent(reg, parameters);
            }
        }
        /// <summary>
        /// Resolves an unregistered service
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static object ResolveUnregistered(this IComponentContext context, Type serviceType)
        {
            return ResolveUnregistered(context, serviceType, Enumerable.Empty<Parameter>());
        }
        /// <summary>
        /// Resolves an unregistered service
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ResolveUnregistered(this IComponentContext context, Type serviceType, params Parameter[] parameters)
        {
            return ResolveUnregistered(context, serviceType, (IEnumerable<Parameter>)parameters);
        }
        /// <summary>
        /// Resolves an unregistered service
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static TService ResolveUnregistered<TService>(this IComponentContext context)
        {
            return (TService)ResolveUnregistered(context, typeof(TService), Enumerable.Empty<Parameter>());
        }
        /// <summary>
        /// Resolves an unregistered service
        /// </summary>
        /// <param name="context"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static TService ResolveUnregistered<TService>(this IComponentContext context, params Parameter[] parameters)
        {
            return (TService)ResolveUnregistered(context, typeof(TService), (IEnumerable<Parameter>)parameters);
        }
    }
}