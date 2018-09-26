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
    public class Claims : IEnumerable<Claim>, IEquatable<Claims>
    {  
        private List<Claim> _claims = new List<Claim>();

        /// <summary>
        /// Gets the empty representation of <see cref="Claims"/>
        /// </summary>
        public static readonly Claims Empty = new Claims(new Claim[0]);

        /// <summary>
        /// Instantiates a set of Claims with the provided claims
        /// </summary>
        /// <param name="claims">The claims to populate</param>
        public Claims(IEnumerable<Claim> claims)
        {
            _claims.AddRange(claims ?? Enumerable.Empty<Claim>());
        }

        /// <summary>
        /// Determines if two Claims objects are equal
        /// True if it contains the same claims ( in any order )
        /// </summary>
        /// <param name="other">The other <see cref="Claims" /> object to compare to</param>
        /// <returns>True if equal, False otherwise</returns>
        public bool Equals(Claims other)
        {
            if(other == null || other.Count() != this.Count())
                return false;

            var thisClaims = _claims.OrderBy(_ => _.Name).ThenBy(_ => _.ValueType).ThenBy(_ => _.Value).ToArray();
            var otherClaims = other.OrderBy(_ => _.Name).ThenBy(_ => _.ValueType).ThenBy(_ => _.Value).ToArray();

            for (int i = 0; i < thisClaims.Count(); i++)
            {
                if (!object.Equals(thisClaims[i], otherClaims[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if two Claims objects are equal
        /// True if it contains the same claims ( in any order )
        /// </summary>
        /// <param name="other">The other object to compare to</param>
        /// <returns>True if equal, False otherwise</returns>
        public override bool Equals(object other)
        {
            return Equals(other as Claims);
        }

        /// <summary>
        /// Gets a HashCode representing the value of all the claims
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            var array = _claims.OrderBy(_ => _.Name).ThenBy(_ => _.ValueType).ThenBy(_ => _.Value).ToArray();
            unchecked
            {
                int hash = 17;

                // get hash code for all items in array
                foreach (var item in array)
                {
                    hash = hash * 23 + ((item != null) ? item.GetHashCode() : 0);
                }

                return hash;
            }
        }


        /// <summary>
        /// Uses the same equality as the Equals method
        /// </summary>
        /// <param name="first">First Claim</param>
        /// <param name="second">Second Claim</param>
        /// <returns>True if equals, false otherwise</returns>
        public static bool operator == (Claims first, Claims second) 
        {
            if(Object.Equals(first,null) && Object.Equals(second,null))
                return true;
            if(Object.Equals(first,null))
                return false;
            return first.Equals(second);
        }

        /// <summary>
        /// Uses the same equality as the Equals method
        /// </summary>
        /// <param name="first">First Claim</param>
        /// <param name="second">Second Claim</param>
        /// <returns>True if not equals, false otherwise</returns>
        public static bool operator != (Claims first, Claims second) 
        {
            return !(first == second);
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