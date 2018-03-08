/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.DependencyInversion.Bootstrap
{
    /// <summary>
    /// The exception that gets thrown if a binding provider does not have a default constructor
    /// </summary>
    public class BindingProviderMustHaveADefaultConstructor : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BindingProviderMustHaveADefaultConstructor"/>
        /// </summary>
        /// <param name="providerType"><see cref="Type"/> of provider</param>
        public BindingProviderMustHaveADefaultConstructor(Type providerType) : base($"{providerType.AssemblyQualifiedName} does not have a default constructor")
        {
        }
    }
}