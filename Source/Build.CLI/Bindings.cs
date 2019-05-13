/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;

namespace Dolittle.Build.CLI
{
    /// <summary>
    /// Represents bindings for the build system
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var buildTarget = Program.BuildTarget;
            if (buildTarget == null) buildTarget = new BuildTarget(null,null,null,null);
            builder.Bind<BuildTarget>().To(buildTarget);
        }
    }
}