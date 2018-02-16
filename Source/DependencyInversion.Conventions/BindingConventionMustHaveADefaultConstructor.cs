/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.DependencyInversion.Conventions
{
    /// <summary>
    /// The exception that gets thrown if a binding convention does not have a default constructor
    /// </summary>
    public class BindingConventionMustHaveADefaultConstructor : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BindingConventionMustHaveADefaultConstructor"/>
        /// </summary>
        /// <param name="conventionType"><see cref="Type"/> of convention</param>
        public BindingConventionMustHaveADefaultConstructor(Type conventionType) : base($"{conventionType.AssemblyQualifiedName} does not have a default constructor")
        {
        }
    }
    
}