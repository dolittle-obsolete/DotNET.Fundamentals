/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Resources
{
    /// <summary>
    /// Defines a factory for creation of <see cref="IResourceDefinitionBuilder"/>
    /// </summary>
    public interface IResourceDefinitionBuilderFactory
    {
        /// <summary>
        /// Create a <see cref="IResourceDefinitionBuilder"/>
        /// </summary>
        /// <returns>A new instance of <see cref="IResourceDefinitionBuilder"/></returns>
        IResourceDefinitionBuilder  Create();
    }
}