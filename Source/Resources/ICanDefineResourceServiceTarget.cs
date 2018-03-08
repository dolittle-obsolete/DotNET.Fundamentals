/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Resources
{
    /// <summary>
    /// Defines the targets for resources
    /// </summary>
    public interface ICanDefineResourceServiceTarget
    {
        /// <summary>
        /// Defines the resource target
        /// </summary>
        /// <param name="builder"><see cref="IResourceDefinitionTargetBuilder"/> to build on</param>
        void Define(IResourceDefinitionTargetBuilder builder);
    }
}