// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Assemblies;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the settings for <see cref="BootStage.InitialSystem"/> stage.
    /// </summary>
    public class DiscoverySettings : IRepresentSettingsForBootStage
    {
        /// <summary>
        /// Gets the <see cref="ICanProvideAssemblies">assembly provider</see> to use.
        /// </summary>
        public ICanProvideAssemblies AssemblyProvider { get; internal set; }
    }
}