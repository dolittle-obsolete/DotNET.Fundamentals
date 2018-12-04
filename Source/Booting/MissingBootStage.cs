/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Booting
{
    /// <summary>
    /// The exception that gets thrown when a <see cref="BootStage"/> is missing
    /// </summary>
    public class MissingBootStage : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingBootStage"/>
        /// </summary>
        /// <param name="bootStage">The <see cref="BootStage"/> that is missing</param>
        public MissingBootStage(BootStage bootStage) : base($"BootStage '{bootStage}' is missing - this could be due to a missing dependency that should be adding the boot stage") {}

    }
}