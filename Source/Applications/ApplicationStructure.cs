/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructure"/>
    /// </summary>
    public class ApplicationStructure : IApplicationStructure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructure"/>
        /// </summary>
        /// <param name="root"><see cref="IApplicationStructureFragment">Root fragment</see> for the structure</param>
        public ApplicationStructure(IApplicationStructureFragment root)
        {
            Root = root;
        }

        /// <inheritdoc/>
        public IApplicationStructureFragment Root { get; }
    }
}