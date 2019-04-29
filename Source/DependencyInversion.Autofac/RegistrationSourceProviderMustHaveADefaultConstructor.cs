/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.DependencyInversion.Autofac
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="ICanProvideRegistrationSources"/> does not have a default constructor
    /// </summary>

    public class RegistrationSourceProviderMustHaveADefaultConstructor : Exception
    {
        /// <summary>
        /// Intializes a new instance of <see cref="RegistrationSourceProviderMustHaveADefaultConstructor"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="ICanProvideRegistrationSources"/> that is missing default constructor</param>
        public RegistrationSourceProviderMustHaveADefaultConstructor(Type type) : base($"Registration source '{type.AssemblyQualifiedName}' is missing a default constructor")
        {
        }
    }
}