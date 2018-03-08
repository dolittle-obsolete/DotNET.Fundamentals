/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Resources
{
    /// <summary>
    /// Represents an implementation of <see cref="IResourceTargetDefinitions"/>
    /// </summary>
    public class ResourceTargetDefinitions : IResourceTargetDefinitions
    {
        /// <inheritdoc/>
        public IEnumerable<IResourceTargetDefinition> GetFor(IResourceDefinition source)
        {
            throw new System.NotImplementedException();
        }
    }
}