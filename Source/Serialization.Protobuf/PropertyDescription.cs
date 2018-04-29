/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using System.Text;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Represents a description of a property, typically found in a <see cref="MessageDescription"/>
    /// </summary>
    public class PropertyDescription
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PropertyDescription"/>
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo"/> representing the actual property</param>
        /// <param name="name">Specific custom name of the property</param>
        /// <param name="defaultValue">The default value for the property if not set</param>
        /// <param name="number">Number representing the property</param>
        public PropertyDescription(PropertyInfo property, string name = null, object defaultValue = null, int number = 0)
        {
            Property = property;
            Name = name??property.Name;
            DefaultValue = defaultValue;
            Number = number == 0 ? GeneratePropertyNumber() : number;
        }

        /// <summary>
        /// Get the name of the property
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get the <see cref="PropertyInfo"/> for the property
        /// </summary>
        public PropertyInfo Property {  get; }

        /// <summary>
        /// The position within a <see cref="MessageDescription">message</see>
        /// </summary>
        /// <remarks>
        /// Positions in Protobuf has a 1 offset
        /// </remarks>
        public int Number { get; }

        /// <summary>
        /// Gets the default value for the property
        /// </summary>
        public object DefaultValue {  get; }

        int GeneratePropertyNumber()
        {
            // https://stackoverflow.com/questions/19985273/get-16-bit-hash-of-a-string-in-c-sharp
            var hasher = System.Security.Cryptography.MD5.Create();
            var data = hasher.ComputeHash(Encoding.UTF8.GetBytes("blah"));
            var propertyHash = BitConverter.ToInt16(data, 0);
            return propertyHash;
        }
        
    }
}