/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        /// Create a custom <see cref="SerializationOptions"/>
        /// </summary>
        /// <param name="flags"><see cref="SerializationOptionsFlags"/> to use</param>
        /// <param name="converters">Any <see cref="IEnumerable{JsonConverter}">converters</see> to use</param>
        /// <param name="callback">A callback for working directly with the <see cref="JsonSerializer"/> for configuring how the serializer works</param>
        /// <returns>An instance of <see cref="SerializationOptions"/></returns>
        public static ISerializationOptions Custom(SerializationOptionsFlags flags=SerializationOptionsFlags.None, IEnumerable<JsonConverter> converters=null, Action<JsonSerializer> callback=null)
        {
            return new SerializationOptions(flags, converters, callback);
        }


        /// <summary>
        /// Initializes a new instance of <see cref="SerializationOptions"/>
        /// </summary>
        /// <param name="flags">The serialization flags</param>
        /// <param name="converters">A collection of additional <see cref="JsonConverter">converters</see></param>
        /// <param name="callback">A callback for working directly with the <see cref="JsonSerializer"/> for configuring how the serializer works</param>
        /// <remarks>
        /// All instances of this class or subclasses must be immutable, because mapping from
        /// serialization options to contract resolvers are cached for performance reasons.
        /// </remarks>
        protected SerializationOptions(
            SerializationOptionsFlags flags,
            IEnumerable<JsonConverter> converters = null,
            Action<JsonSerializer> callback = null)
        {
            Flags = flags;
            if( converters == null ) Converters = new JsonConverter[0];
            else Converters = converters;

            if( callback == null ) Callback = (o) => {};
            else Callback = callback;
        }

        /// <inheritdoc/>
        public virtual bool ShouldSerializeProperty(Type type, string propertyName)
        {
            return true;
        }

        /// <inheritdoc/>
        public SerializationOptionsFlags Flags { get; private set; }

        /// <inheritdoc/>
        public IEnumerable<JsonConverter> Converters { get; }

        /// <inheritdoc/>
        public Action<JsonSerializer> Callback { get; }
    }
}