// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Serialization.Protobuf.for_ValueConverters.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<IValueConverter>> value_converter_instances;
        Establish context = () => value_converter_instances = new Mock<IInstancesOf<IValueConverter>>();
    }
}