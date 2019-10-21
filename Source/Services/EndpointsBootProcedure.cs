/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Booting;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents the <see cref="ICanPerformBootProcedure">boot procedure</see> for <see cref="IEndpoints"/>
    /// </summary>
    public class EndpointsBootProcedure : ICanPerformBootProcedure
    {
        /// <summary>
        /// Gets wether or not all the <see cref="IEndpoint">endpoints</see> are ready
        /// </summary>
        public static bool EndpointsReady { get; private set; } = false;

        readonly IEndpoints _endpoints;

        /// <summary>
        /// Initializes a new instance of <see cref="EndpointsBootProcedure"/>
        /// </summary>
        /// <param name="endpoints">Instance of <see cref="IEndpoints"/> to boot</param>
        public EndpointsBootProcedure(IEndpoints endpoints)
        {
            _endpoints = endpoints;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;


        /// <inheritdoc/>
        public void Perform()
        {
            _endpoints.Start();
            EndpointsReady = true;
        }
    }
}