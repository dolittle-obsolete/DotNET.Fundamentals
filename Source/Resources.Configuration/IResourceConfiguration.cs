/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Resources.Configuration
{
    /// <summary>
    /// Defines the configuration for a resource
    /// </summary>
    public interface IResourceConfiguration
    {
        /// <summary>
        /// Gets the definition of the resource
        /// </summary>
        IResourceDefinition Definition { get; }
    }
}