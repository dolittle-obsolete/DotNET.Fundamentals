// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Machine.Specifications;
using Moq;

namespace Dolittle.Mapping.Specs.for_Maps.given
{
    public class map_for_source_and_target_in_system : all_dependencies
    {
        protected static Maps maps;
        protected static Mock<IMap> map_mock;

        Establish context = () =>
        {
            map_mock = new Mock<IMap>();
            map_mock.SetupGet(m => m.Source).Returns(typeof(Source));
            map_mock.SetupGet(m => m.Target).Returns(typeof(Target));

            map_instances_mock.Setup(m => m.GetEnumerator()).Returns(new IMap[] { map_mock.Object }.AsEnumerable().GetEnumerator());
            maps = new Maps(map_instances_mock.Object);
        };
    }
}
