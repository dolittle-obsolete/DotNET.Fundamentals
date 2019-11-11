/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an identifier for a service aspect
    /// </summary>
    public class ServiceAspect : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="ServiceAspect"/>
        /// </summary>
        /// <param name="type"><see cref="ServiceAspect"/> as string</param>
        public static implicit operator ServiceAspect(string type)
        {
            return new ServiceAspect { Value = type };
        }
    }
}