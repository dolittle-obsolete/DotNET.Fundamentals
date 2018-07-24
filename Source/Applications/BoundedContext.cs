/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents the concept of a bounded context
    /// </summary>
    public class BoundedContext : ConceptAs<Guid>
    { 
         /// <summary>
        /// Implicitly converts from a <see cref="Guid"/> to a <see cref="BoundedContext"/>
        /// </summary>
        /// <param name="boundedContext"><see cref="Guid"/> representing the bounded context</param>
        public static implicit operator BoundedContext(Guid boundedContext)
        {
            return new BoundedContext { Value = boundedContext };
        }       
    }
}