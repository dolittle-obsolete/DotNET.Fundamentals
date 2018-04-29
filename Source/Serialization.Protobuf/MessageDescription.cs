/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Represents the contract of a message
    /// </summary>
    public class MessageDescription
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MessageDescription"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> representing the message</param>
        /// <param name="properties"><see cref="IEnumerable{PropertyDescription}">Property descriptions</see></param>
        /// <param name="name">Name of the message</param>
        public MessageDescription(Type type, IEnumerable<PropertyDescription> properties, string name= null)
        {
            Type = type;
            Properties = properties;
            Name = name??type.Name;
        }

        /// <summary>
        /// Gets the name of the message
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the actual CLR type representing the message
        /// </summary>
        /// <returns></returns>
        public Type Type { get; }

        /// <summary>
        /// Gets the properties for the message
        /// </summary>
        public IEnumerable<PropertyDescription> Properties { get; }

        /// <summary>
        /// Start building a description for 
        /// </summary>
        /// <returns>A new instance of <see cref="MessageDescription"/> representing the type</returns>
        public static MessageDescription For<T>(Func<IMessageDescriptionBuilderFor<T>, IMessageDescriptionBuilderFor<T>> builderCallback)
        {
            IMessageDescriptionBuilderFor<T> builder = new MessageDescriptionBuilderFor<T>(typeof(T).Name);
            builder = builderCallback(builder);
            return builder.Build();
        }

        /// <summary>
        /// Create a default <see cref="MessageDescription"/> with all the properties added by their hashcode
        /// </summary>
        /// <returns>A new instance of <see cref="MessageDescription"/> representing the type</returns>
        public static MessageDescription DefaultFor<T>()
        {
            IMessageDescriptionBuilderFor<T> builder = new MessageDescriptionBuilderFor<T>(typeof(T).Name);
            builder.WithAllProperties();
            return builder.Build();
        }
    }
}