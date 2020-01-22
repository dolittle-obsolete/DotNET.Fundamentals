// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using Dolittle.Reflection;
using Grpc.Core;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="IClientManager"/>.
    /// </summary>
    public class ClientManager : IClientManager
    {
        readonly ICallInvokerManager _callInvokerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientManager"/> class.
        /// </summary>
        /// <param name="callInvokerManager"><see cref="ICallInvokerManager"/> to get <see cref="CallInvoker"/> from.</param>
        public ClientManager(ICallInvokerManager callInvokerManager)
        {
            _callInvokerManager = callInvokerManager;
        }

        /// <inheritdoc/>
        public ClientBase Get(Type type)
        {
            ThrowIfTypeDoesNotImplementClientBase(type);
            var constructor = type.GetConstructor(new[] { typeof(CallInvoker) });
            ThrowIfMissingExpectedConstructorClientType(type, constructor);

            return constructor.Invoke(new[] { _callInvokerManager.GetFor(type) }) as ClientBase;
        }

        void ThrowIfTypeDoesNotImplementClientBase(Type type)
        {
            if (!type.Implements(typeof(ClientBase)))
            {
                throw new TypeDoesNotImplementClientBase(type);
            }
        }

        void ThrowIfMissingExpectedConstructorClientType(Type type, ConstructorInfo constructor)
        {
            if (constructor == null)
            {
                throw new MissingExpectedConstructorForClientType(type);
            }
        }
    }
}