/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Serialization.Json
{
    /// <summary>
    /// Represents the options for serialization
    /// </summary>
    public class SerializationOptions : ISerializationOptions
    {
        /// <summary>
        /// The default serialization options
        /// </summary>
        /// <returns>An instance of <see cref="ISerializationOptions"/></returns>
        public static readonly ISerializationOptions Default = new SerializationOptions(SerializationOptionsFlags.None);

        /// <summary>
        /// Serialization options for using camel case
        /// </summary>
        /// <returns>An instance of <see cref="ISerializationOptions"/></returns>
        public static readonly ISerializationOptions CamelCase = new SerializationOptions(SerializationOptionsFlags.UseCamelCase);

        /// <summary>
        /// Serialization options for including type names
        /// </summary>
        /// <returns>An instance of <see cref="ISerializationOptions"/></returns>
        public static readonly ISerializationOptions IncludeTypeNames = new SerializationOptions(SerializationOptionsFlags.IncludeTypeNames);

        /// <summary>
        /// Initializes a new instance of <see cref="SerializationOptions"/>
        /// </summary>
        /// <param name="flags">The serialization flags</param>
        /// <remarks>
        /// All instances of this class or subclasses must be immutable, because mapping from
        /// serialization options to contract resolvers are cached for performance reasons.
        /// </remarks>
        protected SerializationOptions(SerializationOptionsFlags flags)
        {
            Flags = flags;
        }

        /// <summary>
        /// Will always return true
        /// </summary>
        public virtual bool ShouldSerializeProperty(Type type, string propertyName)
        {
            return true;
        }

        /// <summary>
        /// Gets the serialization flags
        /// </summary>
        public SerializationOptionsFlags Flags { get; private set; }
    }
}