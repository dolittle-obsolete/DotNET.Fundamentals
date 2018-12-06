/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the settings for <see cref="BootStage.BootProcedures"/> stage
    /// </summary>
    public class BootProceduresSettings : IRepresentSettingsForBootStage
    {
        /// <summary>
        /// Gets wether or not it is enabled
        /// </summary>
        /// <value></value>
        public bool Enabled { get; internal set; } = true;
    }
}