/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace Dolittle.Booting
{
    /// <summary>
    /// Defines a system that can deal with all the <see cref="BootStage">boot stages</see>
    /// </summary>
    public interface IBootStages
    {
        /// <summary>
        /// Perform all boot stages
        /// </summary>
        /// <param name="boot"><see cref="Boot"/> details</param>
        /// <returns>All the <see cref="BootStageResult">boot stage results</see></returns>
        IEnumerable<BootStageResult> Perform(Boot boot);
    }
}
