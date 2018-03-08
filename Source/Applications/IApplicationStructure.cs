/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines the structure of an application
    /// </summary>
    public interface IApplicationStructure
    {
        /// <summary>
        /// Gets the root <see cref="IApplicationStructureFragment">location fragment definition</see>
        /// </summary>
        IApplicationStructureFragment Root { get; }
    }
}
