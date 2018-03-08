/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Resources
{
    /// <summary>
    /// Exception that gets thrown if there are multiple resources in the system with the same name
    /// </summary>
    public class MultipleResourcesWithSameName : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultipleResourcesWithSameName"/>
        /// </summary>
        /// <param name="name">Name of resource that exists multiple times</param>
        public MultipleResourcesWithSameName(string name) 
            : base($"Resource with name {name} is defined multiple times")
        {

        }
    }
}