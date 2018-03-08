/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Security
{
    /// <summary>
    /// Defines the builder for building a <see cref="ISecurityDescriptor"/>
    /// </summary>
    public interface ISecurityDescriptorBuilder
    {
        /// <summary>
        /// Gets the <see cref="ISecurityDescriptor"/> that is used by the builder
        /// </summary>
        ISecurityDescriptor Descriptor { get; }
    }
}
