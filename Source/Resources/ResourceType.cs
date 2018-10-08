/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Resources
{
    /// <summary>
    /// Represents the type of a resource (readModels, eventStore, ...)
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