using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Applications.for_ApplicationStructureBuilder
{
    public class when_building_with_root
    {
        static IApplicationStructureBuilder builder;
        static IApplicationStructureFragment root;
        static IApplicationStructure structure;

        Establish context = () => 
        {
            root = Mock.Of<IApplicationStructureFragment>();
            builder = ApplicationStructureBuilder.WithRoot(root);
        };

        Because of = () => structure = builder.Build();

        It should_forward_the_root_to_the_structure = () => structure.Root.ShouldEqual(root);
    }
}