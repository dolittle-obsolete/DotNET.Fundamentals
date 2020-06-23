// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Exception that gets thrown when a reverse call server message is not a ping or a request.
    /// </summary>
    public class ServerMessageIsNotPingOrRequest : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerMessageIsNotPingOrRequest"/> class.
        /// </summary>
        /// <param name="messageType">The <see cref="Type" /> of the server message.</param>
        /// <param name="requestType">The <see cref="Type" /> of the request.</param>
        public ServerMessageIsNotPingOrRequest(Type messageType, Type requestType)
            : base($"Reverse call client received a {messageType} server message that was not a ping or a request of type {requestType}. Cannot handle server message")
        {
        }
    }
}