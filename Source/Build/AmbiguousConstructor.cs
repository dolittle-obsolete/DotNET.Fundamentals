/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Build
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="ICanPerformPostBuildTasks"/> has more than one constructors
    /// </summary>
    public class AmbiguousConstructor : Exception 
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AmbiguousConstructor"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> that has more than one constructor</param>
        public AmbiguousConstructor(Type type) 
            : base($"Post build task performer of type '{type.AssemblyQualifiedName} has more than one constructor - there should either be none or just one.'")
        {

        }
    }
}