/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Machine.Specifications;

namespace Dolittle.Assemblies.Specs.for_Assemblies
{
    public class when_getting_all_assemblies : given.two_assemblies
    {
        static IEnumerable<Assembly> result;

        Because of = () => result = assemblies.GetAll();

        // Comment: ShouldContainOnly() seems to be buggy in MSpec - didn't work here - hence the two assertions instead of one
        // It should_return_the_loaded_assemblies = () => result.ShouldContainOnly(loaded_assemblies);
        It should_return_two_loaded_assemblies = () => result.Count().ShouldEqual(2);
        It should_return_the_loaded_assemblies = () => result.ShouldContain(loaded_assemblies);
    }
}