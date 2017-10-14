/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace doLittle.Resources
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