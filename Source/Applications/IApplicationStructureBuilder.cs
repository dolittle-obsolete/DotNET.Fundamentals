/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Applications
{
    /// <summary>
    /// Defines the builder for <see cref="IApplicationStructure"/>
    /// </summary>
    public interface IApplicationStructureBuilder
    {
        /// <summary>
        /// Build an instance of <see cref="IApplicationStructure"/>
        /// </summary>
        /// <returns>A new instance of <see cref="IApplicationStructure"/></returns>
        IApplicationStructure Build();
    }
}
