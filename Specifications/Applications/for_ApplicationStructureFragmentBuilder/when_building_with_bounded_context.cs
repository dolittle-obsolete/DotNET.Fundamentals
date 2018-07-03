using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationStructureFragmentBuilder
{
    public class when_building_with_bounded_context
    {
        static IApplicationStructureFragment boundedContext;
        static IApplicationStructure structure;

        Establish context = () => 
        {
            var bcMock = new Mock<IApplicationStructureFragment>().
            structure = ApplicationStructureBuilder.WithRoot().Build();
        };
    }
}