// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.IO.Tenants.for_Files
{
    public class when_reading_a_valid_file : given.a_tenant_aware_file_system
    {
        const string path = "some/place/file.txt";
        const string expected_result = "Some result";

        static string result;

        Establish context = () => file_system.Setup(_ => _.ReadAllText(MapPath(path))).Returns(expected_result);

        Because of = () => result = tenant_aware_file_system.ReadAllText(path);

        It should_delegate_to_underlying_file_system_and_return_its_result = () => result.ShouldEqual(expected_result);
    }
}