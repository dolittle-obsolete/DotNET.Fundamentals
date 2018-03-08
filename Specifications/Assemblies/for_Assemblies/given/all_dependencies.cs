/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Assemblies;
using Machine.Specifications;
using Moq;

namespace Dolittle.Assemblies.Specs.for_Assemblies.given
{
    public class all_dependencies
    {
        protected static Mock<IAssemblyProvider>    assembly_provider_mock;

        Establish context = () => assembly_provider_mock = new Mock<IAssemblyProvider>();
    }
}