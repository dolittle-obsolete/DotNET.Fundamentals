/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.DependencyInversion;
using Dolittle.DependencyInversion.Bootstrap;
using Dolittle.Types;

namespace Dolittle.Configuration.Files
{
    /// <summary>
    /// Represents bindings for booting
    /// </summary>
    public class BootBindings : ICanProvideBootBindings
    {
        readonly ITypeFinder _typeFinder;
        readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="BootBindings"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for finding types</param>
        /// <param name="container"><see cref="IContainer"/> for resolving instances</param>
        public BootBindings(ITypeFinder typeFinder, IContainer container)
        {
            _typeFinder = typeFinder;
            _container = container;
        }

        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var parsers = new ConfigurationFileParsers(_typeFinder, _container);
            builder.Bind<IConfigurationFileParsers>().To(parsers);
        }
    }
}