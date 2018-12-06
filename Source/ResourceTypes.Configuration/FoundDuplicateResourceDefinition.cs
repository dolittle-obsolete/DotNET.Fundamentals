/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// The exception that gets thrown when there are duplicate resource representations.
    /// </summary>
    public class FoundDuplicateResourceDefinition : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="FoundDuplicateResourceDefinition"/>
        /// </summary>
        /// <param name="resourceType">The <see cref="ResourceType"/> that had duplicate representations</param>
        /// <returns></returns>
        public FoundDuplicateResourceDefinition(ResourceType resourceType) : base($"Found one or more duplicate resource representations with {typeof(ResourceType).FullName} {resourceType.Value}.")
        {
        }
    }
}