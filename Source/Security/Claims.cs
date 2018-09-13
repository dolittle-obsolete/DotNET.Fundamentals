/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Reflection;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace Dolittle.Security
{

    /// <summary>
    /// Represents a set of <see cref="Claim">Claims</see>
    /// </summary>
    public class Claims : IEnumerable<Claim>
    {  
        private List<Claim> _claims = new List<Claim>();

        /// <summary>
        /// Instantiates a set of Claims with the provided claims
        /// </summary>
        /// <param name="claims">The claims to populate</param>
        public Claims(IEnumerable<Claim> claims)
        {
            _claims.AddRange(claims ?? Enumerable.Empty<Claim>());
        }

        /// <summary>
        /// Gets an enumerator to iterate over the claims
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Claim> GetEnumerator()
        {
            return _claims.GetEnumerator();;
        }

        /// <summary>
        /// Gets an enumerator to iterate over the claims
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _claims.GetEnumerator();
        }
    }
}