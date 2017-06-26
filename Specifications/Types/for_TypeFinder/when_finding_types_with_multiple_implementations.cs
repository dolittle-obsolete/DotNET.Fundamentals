/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace doLittle.Types.Specs.for_TypeFinder
{
    [Subject(typeof(TypeFinder))]
    public class when_finding_types_with_multiple_implementations : given.a_type_finder
    {
        static IEnumerable<Type> types_found;

        Establish context = () => contract_to_implementors_map_mock.Setup(c => c.GetImplementorsFor(typeof(IMultiple))).Returns(new[] { typeof(FirstMultiple), typeof(SecondMultiple) });

        Because of = () => types_found = type_finder.FindMultiple<IMultiple>();

        It should_not_return_null = () => types_found.ShouldNotBeNull();
        It should_contain_the_expected_types = () => types_found.ShouldContain(typeof(FirstMultiple), typeof(SecondMultiple));
    }
}