/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Represents a description of a property, typically found in a <see cref="MessageDescription"/>
    /// </summary>
    public class PropertyDescription
    {
        /// <summary>
        /// Get the name of the property
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get the <see cref="PropertyInfo"/> for the property
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// The position within a <see cref="MessageDescription">message</see>
        /// </summary>
        public int Position { get; }
    }
}