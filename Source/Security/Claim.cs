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
using Dolittle.Concepts;

namespace Dolittle.Security
{
    
    /// <summary>
    /// Represents a Claim
    /// </summary>
    public class Claim : Value<Claim>
    {
        /// <summary>
        /// Instantiates an instance of a Claim
        /// </summary>
        /// <param name="name">The Name of the claim</param>
        /// <param name="value">The Value of the claim</param>
        /// <param name="valueType">The type of the Value of the claim</param>
        public Claim(string name, string value, string valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }

        /// <summary>
        /// The Name of the claim
        /// </summary>
        /// <value></value>
        public string Name { get; }
        /// <summary>
        /// The Value of the claim
        /// </summary>
        /// <value></value>
        public string Value { get; }
        /// <summary>
        /// The type of the Value of the claim
        /// </summary>
        /// <value></value>
        public string ValueType { get; }

        /// <summary>
        /// Converts the <see cref="Claim" /> instance into the corresponding <see cref="System.Security.Claims.Claim" /> instance
        /// </summary>
        /// <returns>a <see cref="System.Security.Claims.Claim" /> instance</returns>
        public System.Security.Claims.Claim ToDotnetClaim()
        {
            return new System.Security.Claims.Claim(Name,Value,ValueType);
        }

        /// <summary>
        /// Converts the <see cref="System.Security.Claims.Claim" /> instance into the corresponding <see cref="Dolittle.Security.Claim" /> instance
        /// </summary>
        /// <returns>a <see cref="Dolittle.Security.Claim" /> instance</returns>
        public static Dolittle.Security.Claim FromDotnetClaim(System.Security.Claims.Claim claim)
        {
            if(claim == null)
                return null;

            return new Dolittle.Security.Claim(claim.Type, claim.Value, claim.ValueType);
        }
    }
}