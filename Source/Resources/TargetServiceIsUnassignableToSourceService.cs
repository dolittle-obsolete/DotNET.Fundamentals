/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Resources
{
    /// <summary>
    /// Exception that gets thrown if a <see cref="ResourceServiceTarget"/> is not assignable to <see cref="ResourceService"/>
    /// </summary>
    public class TargetServiceIsUnassignableToSourceService : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TargetServiceIsUnassignableToSourceService"/>
        /// </summary>
        /// <param name="source"><see cref="Type">Source service</see></param>
        /// <param name="target"><see cref="Type">Target implementation</see></param>
        public TargetServiceIsUnassignableToSourceService(Type source, Type target) : base($"Target service of type '{target.AssemblyQualifiedName}' is not assignable to source service of type '{source.AssemblyQualifiedName}'") {}
   }
}