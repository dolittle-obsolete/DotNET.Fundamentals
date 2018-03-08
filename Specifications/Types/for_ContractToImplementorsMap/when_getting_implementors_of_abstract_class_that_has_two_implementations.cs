/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.Types.Specs.for_ContractToImplementorsMap
{
    public class when_getting_implementors_of_abstract_class_that_has_two_implementations : given.an_empty_map
    {
        static IEnumerable<Type> result;

        Establish context = () => map.Feed(new[] { typeof(ImplementationOfAbstractClass), typeof(SecondImplementationOfAbstractClass) });

        Because of = () => result = map.GetImplementorsFor(typeof(AbstractClass));

        It should_have_both_the_implementations_only = () => result.ShouldContainOnly(typeof(ImplementationOfAbstractClass), typeof(SecondImplementationOfAbstractClass));
    }
}
