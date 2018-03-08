/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Dolittle.Assemblies;
using Machine.Specifications;
using Moq;

namespace Dolittle.Types.Specs.for_TypeFinder.given
{
    public class a_type_finder
    {
        protected static TypeFinder type_finder;
        protected static Type[] types;

        protected static Mock<Assembly> assembly_mock;
        protected static Mock<IAssemblies> assemblies_mock;
        protected static Mock<IContractToImplementorsMap> contract_to_implementors_map_mock;

        Establish context = () =>
        {
            types = new[] {
                typeof(ISingle),
                typeof(Single),
                typeof(IMultiple),
                typeof(FirstMultiple),
                typeof(SecondMultiple)
            };

            assembly_mock = new Mock<Assembly>();
            assembly_mock.Setup(a => a.GetTypes()).Returns(types);
            assembly_mock.Setup(a => a.FullName).Returns("A.Full.Name");

            assemblies_mock = new Mock<IAssemblies>();
            assemblies_mock.Setup(x => x.GetAll()).Returns(new[] { assembly_mock.Object });

            contract_to_implementors_map_mock = new Mock<IContractToImplementorsMap>();
            contract_to_implementors_map_mock.SetupGet(c => c.All).Returns(types);            
            type_finder = new TypeFinder(assemblies_mock.Object, contract_to_implementors_map_mock.Object);
        };
    }
}
