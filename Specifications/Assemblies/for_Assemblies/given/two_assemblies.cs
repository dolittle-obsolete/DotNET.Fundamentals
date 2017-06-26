/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using doLittle.Assemblies;
using Machine.Specifications;
using Moq;

namespace doLittle.Assemblies.Specs.for_Assemblies.given
{
    public class two_assemblies : all_dependencies
    {
        protected static IEnumerable<AssemblyName>   assembly_names;
        protected static Assemblies assemblies;

        protected static AssemblyName first_assembly_name;
        protected static AssemblyName second_assembly_name;
        protected static Mock<Assembly> first_assembly_mock;
        protected static Mock<Assembly> second_assembly_mock;
        protected static IEnumerable<Assembly> loaded_assemblies;

        Establish context = () => 
        {
            first_assembly_name = new AssemblyName("First, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            second_assembly_name = new AssemblyName("Second, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            assembly_names = new [] {
                first_assembly_name,
                second_assembly_name
            };
            first_assembly_mock = new Mock<Assembly>();
            first_assembly_mock.Setup(a => a.GetName()).Returns(first_assembly_name);
            first_assembly_mock.Setup(a => a.FullName).Returns(first_assembly_name.FullName);
            first_assembly_mock.Setup(a => a.ToString()).Returns(first_assembly_name.Name);

            second_assembly_mock = new Mock<Assembly>();
            second_assembly_mock.Setup(a => a.GetName()).Returns(second_assembly_name);
            second_assembly_mock.Setup(a => a.FullName).Returns(second_assembly_name.FullName);
            second_assembly_mock.Setup(a => a.ToString()).Returns(second_assembly_name.Name);

            loaded_assemblies = new [] {
                first_assembly_mock.Object,
                second_assembly_mock.Object
            };

            assembly_provider_mock.Setup(a => a.GetAllByName()).Returns(assembly_names);
            assembly_provider_mock.Setup(a => a.Load(first_assembly_name)).Returns(first_assembly_mock.Object);
            assembly_provider_mock.Setup(a => a.Load(second_assembly_name)).Returns(second_assembly_mock.Object);
            assemblies = new Assemblies(assembly_provider_mock.Object); 
        };
    }
}