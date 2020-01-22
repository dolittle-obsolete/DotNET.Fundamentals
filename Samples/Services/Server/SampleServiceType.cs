// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Services;

namespace Server
{
    public class SampleServiceType : IRepresentServiceType
    {
        public ServiceType Identifier => "Sample";

        public Type BindingInterface => typeof(ICanBindSampleServices);

        public EndpointVisibility Visibility => EndpointVisibility.Private;
    }
}
