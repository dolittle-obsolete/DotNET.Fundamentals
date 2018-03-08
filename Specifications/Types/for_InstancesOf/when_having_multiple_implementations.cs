/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Dolittle.DependencyInversion;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Types.Specs.for_InstancesOf
{
    public class when_having_multiple_implementations
    {
        static Mock<ITypeFinder> type_finder;
        static Mock<IContainer> container;
        static IAmAnInterface[] instances;

        static OneImplementation one_implementation_instance;
        static SecondImplementation second_implemenation_instance;

        Establish context = () => 
        {
            type_finder = new Mock<ITypeFinder>();
            type_finder.Setup(t => t.FindMultiple<IAmAnInterface>()).Returns(new Type[] {
                typeof(OneImplementation),
                typeof(SecondImplementation)
            });
            container = new Mock<IContainer>();
            one_implementation_instance = new OneImplementation();
            container.Setup(c => c.Get(typeof(OneImplementation))).Returns(one_implementation_instance);
            second_implemenation_instance = new SecondImplementation();
            container.Setup(c => c.Get(typeof(SecondImplementation))).Returns(second_implemenation_instance);
        };

        Because of = () => instances = new InstancesOf<IAmAnInterface>(type_finder.Object, container.Object).ToArray();

        It should_get_the_implementations = () => instances.ShouldContainOnly(one_implementation_instance, second_implemenation_instance);
             
    }
}
