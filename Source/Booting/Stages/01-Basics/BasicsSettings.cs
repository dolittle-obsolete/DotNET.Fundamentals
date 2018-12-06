/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Execution;
using System.Reflection;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the settings for <see cref="BootStage.Basics"/> stage
    /// </summary>
    public class BasicsSettings : IRepresentSettingsForBootStage
    {
        /// <summary>
        /// Gets the <see cref="Environment"/> we're running in
        /// </summary>
        public Environment Environment {  get; internal set;  }

        /// <summary>
        /// Gets the entry <see cref="Assembly"/>
        /// </summary>
        /// <value></value>
        public Assembly EntryAssembly { get; internal set;  }
    }
}
