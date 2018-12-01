/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.ResourceTypes
{
    /// <summary>
    /// Represents the type of a resource in the system
    /// </summary>
    public class ResourceType : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to an <see cref="ResourceType"/>
        /// </summary>
        public static implicit operator ResourceType(string value)
        {
            return new ResourceType { Value = value };
        }
    }
}