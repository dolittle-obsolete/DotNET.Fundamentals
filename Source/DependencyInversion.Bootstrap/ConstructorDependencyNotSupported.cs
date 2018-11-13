/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.DependencyInversion.Bootstrap
{
    /// <summary>
    /// The exception that gets thrown when a constructor has dependencies that aren't supported
    /// </summary>
    public class ConstructorDependencyNotSupported : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConstructorDependencyNotSupported"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> that holds the constructor</param>
        /// <param name="dependency"><see cref="Type"/> the wrong dependency</param>
        /// <param name="supportedDependencies"><see cref="IEnumerable{T}">Collection</see> of <see cref="Type">supported types</see></param>
        public ConstructorDependencyNotSupported(Type type, Type dependency, IEnumerable<Type> supportedDependencies) 
            : base($"Constructor for '{type.AssemblyQualifiedName}' has an supported dependency of '{dependency.AssemblyQualifiedName}.\nSupported dependencies are {string.Join(".", supportedDependencies)}'")
        {

        }
    }
}