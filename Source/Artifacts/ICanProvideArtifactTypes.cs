/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace doLittle.Artifacts
{
    /// <summary>
    /// Defines a system that can provide <see cref="IArtifactType"/>
    /// </summary>
    public interface ICanProvideArtifactTypes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         IEnumerable<IArtifactType> Provide();
    }
}