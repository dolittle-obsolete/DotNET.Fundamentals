/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Resources
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="ResourceServiceTarget"/> is missing by name
    /// </summary>
    public class MissingServiceTarget : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingServiceTarget"/>
        /// </summary>
        /// <param name="resource"><see cref="IResourceDefinition"/> the target should be for</param>
        /// <param name="name">Expected name of the target</param>
        public MissingServiceTarget(IResourceDefinition resource, string name) : base($"Missing service target '{name}' for service '{resource.Name}'")
        {

        }

    }
}