/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Booting;

namespace Dolittle.DependencyInversion.Booting.Stages
{
    /// <summary>
    /// Represents the settings for <see cref="BootStage.Container"/> stage
    /// </summary>
    public class ContainerSettings : IRepresentSettingsForBootStage
    {
        /// <summary>
        /// Gets the <see cref="IContainer"/> type
        /// </summary>
        public Type ContainerType { get; internal set; }
    }
}