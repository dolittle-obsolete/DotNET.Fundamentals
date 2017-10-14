/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace doLittle.Resources
{
    /// <summary>
    /// Defines a system for working with <see cref="IResourceTargetDefinition"/>
    /// </summary>
    public interface IResourceTargetDefinitions
    {
        /// <summary>
        /// Get <see cref="IResourceTargetDefinition"/> for <see cref="IResourceDefinition"/>
        /// </summary>
        /// <param name="source"><see cref="IResourceDefinition"/> as source</param>
        /// <returns><see cref="IEnumerable{IResourceTargetDefinition}">Resource target definitions</see> for the <see cref="IResourceDefinition"/></returns>
        IEnumerable<IResourceTargetDefinition> GetFor(IResourceDefinition source);
    }
}