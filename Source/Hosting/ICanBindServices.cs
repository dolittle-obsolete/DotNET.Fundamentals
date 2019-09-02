/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Grpc.Core;

namespace Dolittle.Hosting
{
    /// <summary>
    /// Defines the generic interface for binding gRPC services
    /// </summary>
    public interface ICanBindServices
    {
        /// <summary>
        /// Binds the services and returns the <see cref="Service"/>
        /// </summary>
        /// <returns><see cref="IEnumerable{Service}">Collection of </see></returns>
        IEnumerable<Service> BindServices();
    }
}