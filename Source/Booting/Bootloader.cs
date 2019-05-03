/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Assemblies;
using Dolittle.DependencyInversion;
using Dolittle.Types;

namespace Dolittle.Booting
{
    /// <summary>
    /// Represents the starting point - the actual boot of an application with configuration options
    /// for what is possible to configure before actual booting
    /// </summary>
    public class Bootloader
    {
        readonly Boot _boot;

        /// <summary>
        /// Initializes a new instance of <see cref="Bootloader"/>
        /// </summary>
        /// <param name="boot"></param>
        public Bootloader(Boot boot)
        {
            _boot = boot;
        }

        /// <summary>
        /// Start booting
        /// </summary>
        public BootloaderResult Start()
        {
            var bootStages = new BootStages();
            var result = bootStages.Perform(_boot);
            var bootloaderResult = new BootloaderResult(
                result.Container,
                result.GetAssociation(WellKnownAssociations.TypeFinder) as ITypeFinder,
                result.GetAssociation(WellKnownAssociations.Assemblies) as IAssemblies,
                result.GetAssociation(WellKnownAssociations.Bindings) as IBindingCollection,
                result.BootStageResults
            );

            return bootloaderResult;
        }

        /// <summary>
        /// Configure boot
        /// </summary>
        /// <param name="builderDelegate">Builder delegete</param>
        /// <returns><see cref="Bootloader"/> for booting</returns>
        public static Bootloader Configure(Action<IBootBuilder> builderDelegate)
        {
            var builder = new BootBuilder();
            builderDelegate(builder);
            var boot = builder.Build();
            var bootLoader = new Bootloader(boot);
            return bootLoader;
        }
    }
}
