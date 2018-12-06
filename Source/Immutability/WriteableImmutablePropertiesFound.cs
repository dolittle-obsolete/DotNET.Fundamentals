/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dolittle.Immutability
{
    /// <summary>
    /// The exception that gets thrown when an <see cref="IAmImmutable">immutable object</see> is mutable 
    /// by virtue of it having properties that can be written to
    /// </summary>
    public class WriteableImmutablePropertiesFound : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="properties"></param>
        public WriteableImmutablePropertiesFound(Type type, IEnumerable<PropertyInfo> properties) 
            : base($"Type '{type.AssemblyQualifiedName}' has writeable properties called '{string.Join(",",properties.Select(_ => _.Name))}' - this is not allowed for immutable objects")
        {

        }
    }

}