/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Resources
{
    /// <summary>
    /// Defines the builder for building <see cref="ResourceDefinition"/>
    /// </summary>
    public interface IResourceDefinitionBuilder
    {
        /// <summary>
        /// Names the <see cref="IResourceDefinition"/>
        /// </summary>
        /// <param name="name">Name of the <see cref="IResourceDefinition"/> being built</param>
        /// <returns>Chained <see cref="IResourceDefinitionBuilder"/></returns>
        IResourceDefinitionBuilder  WithName(string name);

        /// <summary>
        /// Indicates that a specific service is required in the <see cref="IResourceDefinition"/>
        /// </summary>
        /// <returns>Chained <see cref="IResourceDefinitionBuilder"/></returns>
        IResourceDefinitionBuilder Requires<T>();
        

        /// <summary>
        /// Build an instance of <see cref="ResourceDefinition"/>
        /// </summary>
        /// <returns>A new instance of <see cref="ResourceDefinition"/></returns>
        IResourceDefinition Build();
    }
}