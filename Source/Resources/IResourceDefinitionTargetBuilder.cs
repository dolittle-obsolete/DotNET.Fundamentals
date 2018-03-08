/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Resources
{
    /// <summary>
    /// Defines the builder for building <see cref="IResourceTargetDefinition"/>
    /// </summary>
    public interface IResourceDefinitionTargetBuilder
    {
        /// <summary>
        /// Build an instance of <see cref="IResourceTargetDefinition"/>
        /// </summary>
        /// <returns>A new instance of <see cref="IResourceTargetDefinition"/></returns>
        IResourceTargetDefinition Build();
    }
}