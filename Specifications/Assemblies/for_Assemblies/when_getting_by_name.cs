// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Machine.Specifications;

namespace Dolittle.Assemblies.Specs.for_Assemblies
{
    public class when_getting_by_name : given.two_assemblies
    {
        static Assembly result;
        Because of = () => result = assemblies.GetByName(second_assembly_name.Name);

        It should_return_correct_assembly = () => result.ShouldEqual(second_assembly_mock.Object);
    }
}