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
        readonly IEndpoints _hosts;

        /// <summary>
        /// Initializes a new instance of <see cref="EndpointsBootProcedure"/>
        /// </summary>
        /// <param name="hosts">Instance of <see cref="IEndpoints"/> to boot</param>
        public EndpointsBootProcedure(IEndpoints hosts)
        {
            _hosts = hosts;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;


        /// <inheritdoc/>
        public void Perform()
        {
            _hosts.Start();
        }
    }
}