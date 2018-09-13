/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using System.Security.Claims;

namespace Dolittle.Security
{

    /// <summary>
    /// Extensions for ClaimsPrincipal
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        ///  Creates a <see cref="Claims"/> instance from a Claims Principal.
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns>a <see cref="Claims"/> instance</returns>
        public static Claims ToClaims(this ClaimsPrincipal claimsPrincipal)
        {
            if(claimsPrincipal == null)
                return null;

            return new Claims(claimsPrincipal.Claims.Select(c => new Dolittle.Security.Claim(c.Type, c.Value, c.ValueType)));
        }

        /// <summary>
        ///  Creates a <see cref="ClaimsPrincipal"/> instance from a <see cref="Claims" /> instance.
        /// </summary>
        /// <param name="claims"></param>
        /// <returns>a <see cref="ClaimsPrincipal"/> instance</returns>
        public static ClaimsPrincipal ToClaimsPrincipal(this Claims claims)
        {
            if(claims == null)
                return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity(claims.Select(c => c.ToDotnetClaim())));
        }        
    }    
}