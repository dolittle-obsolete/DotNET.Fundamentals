/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Autofac;
using Dolittle.Assemblies;

namespace Dolittle.DependencyInversion.Autofac
{
    /// <summary>
    /// Represents async implementation of <see cref="ICanProvideContainer"/> specific for Autofac
    /// </summary>
    public class ContainerProvider : ICanProvideContainer
    {
        /// <inheritdoc/>
        public IContainer Provide(IAssemblies assemblies, IBindingCollection bindings)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.AddDolittle(assemblies, bindings);
            var autofacContainer = containerBuilder.Build();
            var container = new Container(autofacContainer);
            return container;
        }
    }
}