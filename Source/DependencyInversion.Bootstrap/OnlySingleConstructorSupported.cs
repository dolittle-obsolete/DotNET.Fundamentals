/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.DependencyInversion.Bootstrap
{
    /// <summary>
    /// The exception that gets thrown when there are more than one constructors and only one is supported
    /// </summary>
    public class OnlySingleConstructorSupported : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OnlySingleConstructorSupported"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> that has more than one constructor</param>
        public OnlySingleConstructorSupported(Type type) : base($"'{type.AssemblyQualifiedName}' has more than one constructor - only one is supported")
        {
        }
    }
}