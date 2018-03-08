/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="ApplicationStructureFragment"/> gets a type that
    /// has a parent but it does not belong to the parent
    /// </summary>
    public class ApplicationStructureFragmentMustBelongToParent : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureFragmentMustBelongToParent"/>
        /// </summary>
        /// <param name="parent">Parent <see cref="Type"/></param>
        /// <param name="child">Parent <see cref="Type"/></param>
        public ApplicationStructureFragmentMustBelongToParent(Type parent, Type child) 
            : base($"Type '{child.AssemblyQualifiedName}' needs to implement the '{typeof(IBelongToAnApplicationLocationSegmentTypeOf<>).AssemblyQualifiedName}' for parent type '{parent.AssemblyQualifiedName}'")
        {
        }
    }
}