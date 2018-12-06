/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Execution;
using System.Reflection;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the <see cref="BootStage.Basics"/> stage of booting
    /// </summary>
    public class Basics : ICanPerformBootStage<BasicsSettings>
    {
        /// <inheritdoc/>
        public BootStage BootStage => BootStage.Basics;

        /// <inheritdoc/>
        public void Perform(BasicsSettings settings, IBootStageBuilder builder)
        {
            var entryAssembly = settings.EntryAssembly ?? Assembly.GetEntryAssembly();
            var environment = settings.Environment ?? Environment.Production;

            builder.Associate(WellKnownAssociations.EntryAssembly, entryAssembly);
            builder.Associate(WellKnownAssociations.Environment, environment);
            builder.Bindings.Bind<Environment>().To(environment);
        }
    }
}
