/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// Exception that gets thrown when a parent <see cref="IApplicationStructureFragment"/> can't
    /// hold specific <see cref="IApplicationLocationSegment"/>
    /// </summary>
    public class ParentApplicationStructureFragmentMustBeAbleToHoldType : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public ParentApplicationStructureFragmentMustBeAbleToHoldType(Type parent, Type child)
            : base($"Parent '{parent.AssemblyQualifiedName}' is not able to hold '{child.AssemblyQualifiedName}' - make sure parent implements {typeof(ICanHoldApplicationLocationSegmentsOfType<>).AssemblyQualifiedName}")
        {
        }
    }
}