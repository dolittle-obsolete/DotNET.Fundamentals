/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications
{
    /// <summary>
    /// Defines the builder for <see cref="IApplicationStructure"/>
    /// </summary>
    public interface IApplicationStructureBuilder
    {
        /// <summary>
        /// Build an instance of <see cref="IApplicationStructure"/> with the default <see cref="IApplicationStructureValidationStrategy"/>
        /// </summary>
        /// <returns>A new instance of <see cref="IApplicationStructure"/></returns>
        IApplicationStructure Build();
        /// <summary>
        /// Build an instance of <see cref="IApplicationStructure"/>
        /// </summary>
        /// <returns>A new instance of <see cref="IApplicationStructure"/></returns>
        IApplicationStructure Build(IApplicationStructureValidationStrategy validationStrategy);
    }
}
