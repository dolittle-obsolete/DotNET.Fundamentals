/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dolittle.Lifecycle;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Represents an implementation of <see cref="IMessageDescriptions"/>
    /// </summary>
    [Singleton]
    public class MessageDescriptions : IMessageDescriptions
    {
        readonly Dictionary<Type, MessageDescription>   _descriptions = new Dictionary<Type, MessageDescription>();

        /// <inheritdoc/>
        public MessageDescription GetFor<T>()
        {
            if( HasFor<T>() ) return _descriptions[typeof(T)];
            var description = MessageDescription.DefaultFor<T>();
            SetFor<T>(description);
            return description;
        }

        /// <inheritdoc/>
        public bool HasFor<T>() => _descriptions.ContainsKey(typeof(T));

        /// <inheritdoc/>
        public void SetFor<T>(MessageDescription description)
        {
            ThrowIfTypeMismatchForMessageDescription<T>(description);
            ThrowIfTypeHasMessageDescriptionRegistered<T>();
            _descriptions[typeof(T)] = description;
        }

        void ThrowIfTypeHasMessageDescriptionRegistered<T>()
        {
            if (HasFor<T>()) throw new MessageDescriptionAlreadyRegisteredForType(typeof(T));
        }

        void ThrowIfTypeMismatchForMessageDescription<T>(MessageDescription description)
        {
            if (typeof(T) != description.Type) throw new TypeMismatchForMessageDescription(typeof(T), description.Type);
        }
    }
}