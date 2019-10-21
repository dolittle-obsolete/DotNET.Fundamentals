/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an identifier for a service type
    /// </summary>
    public class ServiceType : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="ServiceType"/>
        /// </summary>
        /// <param name="type"><see cref="ServiceType"/> as string</param>
        public static implicit operator ServiceType(string type)
        {
            return new ServiceType { Value = type };
        }
    }
}