// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Services;

namespace Server
{
    public class SampleServices : ICanBindSampleServices
    {
        readonly TestService _testService;

        public SampleServices(TestService testService)
        {
            _testService = testService;
        }

        public ServiceAspect Aspect => "Friendliness";

        public IEnumerable<Service> BindServices()
        {
            return new Service[]
            {
                new Service(_testService, Contracts.TestService.BindService(_testService), Contracts.TestService.Descriptor)
            };
        }
    }
}
