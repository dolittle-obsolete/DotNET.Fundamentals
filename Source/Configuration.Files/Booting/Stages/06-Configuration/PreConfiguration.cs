/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Booting;
using Dolittle.DependencyInversion;
using Dolittle.DependencyInversion.Booting;
using Dolittle.Types;

namespace Dolittle.Configuration.Files.Booting.Stages
{
    /// <summary>
    /// Represents bindings for booting
    /// </summary>
    public class PreConfiguration : ICanProvideBootBindings //ICanRunBeforeBootStage<NoSettings>
    {
        /// <inheritdoc/>
        public BootStage BootStage => BootStage.Configuration;

        readonly ITypeFinder _typeFinder;
        private readonly IContainer _container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFinder"></param>
        /// <param name="container"></param>
        public PreConfiguration(ITypeFinder typeFinder, IContainer container)
        {
            _typeFinder = typeFinder;
            _container = container;
        }

        /// <inheritdoc/>
        //public void Perform(NoSettings settings, IBootStageBuilder builder)
        public void Provide(IBindingProviderBuilder builder)
        {
            //var typeFinder = builder.GetAssociation(WellKnownAssociations.TypeFinder) as ITypeFinder;
            var parsers = new ConfigurationFileParsers(_typeFinder, _container);
            builder.Bind<IConfigurationFileParsers>().To(parsers);
        }
    }
}
