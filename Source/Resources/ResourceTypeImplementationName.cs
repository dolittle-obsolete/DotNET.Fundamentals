/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceTypeImplementation : ConceptAs<string>
    {

        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to an <see cref="ResourceTypeImplementation"/>
        /// </summary>
        public static implicit operator ResourceTypeImplementation(string value)
        {
            return new ResourceTypeImplementation { Value = value };
        }
    }
}