// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Grpc.Core;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Defines a manager of <see cref="ClientBase">clients</see>.
    /// </summary>
    public interface IClientManager
    {
        /// <summary>
        /// Get a specific type of <see cref="ClientBase"/>.
        /// </summary>
        /// <param name="type">Type of <see cref="ClientBase"/> to get.</param>
        /// <returns><see cref="ClientBase"/> instance.</returns>
        ClientBase Get(Type type);
    }
}