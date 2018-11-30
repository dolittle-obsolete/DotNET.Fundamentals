/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Represents the necessary information to perform a boot
    /// </summary>
    public class Boot
    {
        /// <summary>
        /// Gets all the <see cref="IRepresentSettingsForBootStage">settings</see>
        /// </summary>
        public IEnumerable<IRepresentSettingsForBootStage> Settings { get; }
       
    }
}
