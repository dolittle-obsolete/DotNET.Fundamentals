// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Services;
using Dolittle.Services.Clients;
using static Contracts.TestService;

namespace Client
{
    public class SampleServiceClients : IKnowAboutClients
    {
        public IEnumerable<Dolittle.Services.Clients.Client> Clients => new[]
        {
            new Dolittle.Services.Clients.Client(EndpointVisibility.Private, typeof(TestServiceClient), Descriptor)
        };
    }
}
