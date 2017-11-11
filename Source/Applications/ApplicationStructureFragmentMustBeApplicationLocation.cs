/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Applications
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="ApplicationStructureFragment"/> type is not derived from <see cref="IApplicationLocation"/>
    /// </summary>
    public class ApplicationStructureFragmentMustBeApplicationLocation : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureFragmentMustBeApplicationLocation"/>
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that is wrong</param>
        public ApplicationStructureFragmentMustBeApplicationLocation(Type type)
            : base($"Type '{type.AssemblyQualifiedName}' is not an {typeof(IApplicationLocation).AssemblyQualifiedName}")
        {
        }
    }
}