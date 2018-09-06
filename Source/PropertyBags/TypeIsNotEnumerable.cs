/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.Serialization;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// The exception that gets thrown when a type was expected to be an enumerable, but wasn't
    /// </summary>
    public class TypeIsNotEnumerable : Exception
    {

        /// <summary>
        /// Instantiates an instance of <see cref="TypeIsNotEnumerable"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TypeIsNotEnumerable(Type type) : base($"The type {type.FullName} was expected to be an Enumerable, but wasn't")
        {
        }
    }
}