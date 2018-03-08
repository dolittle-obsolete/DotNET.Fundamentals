/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace Dolittle.Types.Specs.for_TypeFinder
{
    [Subject(typeof(TypeFinder))]
    public class when_finding_type_by_name_that_does_not_exist : given.a_type_finder
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => type_finder.FindTypeByFullName(typeof(Single).FullName + "Blah"));

        It should_be_throw_unable_to_resolve_type_by_name = () => exception.ShouldBeOfExactType<UnableToResolveTypeByName>();
    }
}