/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace doLittle.Resources
{
    /// <summary>
    /// Defines the basis for services needed for a resource
    /// </summary>
    public interface ICanDefineResource
    {
        /// <summary>
        /// Defines the resource
        /// </summary>
        /// <param name="builder"><see cref="IResourceDefinitionBuilder">Builder</see> to build on</param>
        void Define(IResourceDefinitionBuilder builder);
    }
}