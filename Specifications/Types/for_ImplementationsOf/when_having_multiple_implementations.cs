/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Types.Specs.for_ImplementationsOf
{
    public class when_having_multiple_implementations
    {
        static Mock<ITypeFinder>   type_finder_mock;
        static Type[] instances;

        Establish context = () => 
        {
            type_finder_mock = new Mock<ITypeFinder>();
            type_finder_mock.Setup(t => t.FindMultiple<IAmAnInterface>()).Returns(new Type[] {
                typeof(OneImplementation),
                typeof(SecondImplementation)
            });
        };

        Because of = () => instances = new ImplementationsOf<IAmAnInterface>(type_finder_mock.Object).ToArray();

        It should_get_the_implementations = () => instances.ShouldContainOnly(new[] { typeof(OneImplementation), typeof(SecondImplementation)});
    }
}
