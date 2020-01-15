// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Serialization.Protobuf;
using Machine.Specifications;
using Moq;

namespace Dolittle.Concepts.Serialization.Protobuf.for_Serializer.given
{
    public class all_dependencies
    {
        protected static Mock<IMessageDescriptions> message_descriptions;

        Establish context = () => message_descriptions = new Mock<IMessageDescriptions>();
    }
}