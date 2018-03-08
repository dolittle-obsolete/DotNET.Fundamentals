/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// Exception that gets thrown if an <see cref="IApplicationLocationSegment"/> does not have a default
    /// constructor that either takes a string name or a specific type given in the <see cref="IApplicationLocationSegment{T}"/>
    /// </summary>
    public class ApplicationLocationSegmentMustHaveADefaultConstructorTakingName : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationLocationSegmentMustHaveADefaultConstructorTakingName"/>
        /// </summary>
        /// <param name="type">Violating <see cref="Type"/></param>
        /// <param name="parameterType"><see cref="Type"/> of valid parameter</param>
        public ApplicationLocationSegmentMustHaveADefaultConstructorTakingName(Type type, Type parameterType)
            : base($"Type '{type.AssemblyQualifiedName} does not have a default constructor taking the name as a `{parameterType.AssemblyQualifiedName}` of the segment'")
        {

        }
    }
}