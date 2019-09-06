/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an identifier for a host type
    /// </summary>
    public class HostType : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="HostType"/>
        /// </summary>
        /// <param name="type"><see cref="HostType"/> as string</param>
        public static implicit operator HostType(string type)
        {
            return new HostType { Value = type };
        }
    }
}