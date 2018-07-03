using Machine.Specifications;
using System.Linq;
namespace Dolittle.Applications.Specs.for_ApplicationBuilder
{
    public class when_building_with_structure_starting_with_Feature : given.an_ApplicationBuilder
    {
        static IApplication application;

        Because of = () => 
            application = application_builder
                .WithStructureStartingWith<Feature>(b => 
                    b.Required)
            .Build();
        It should_have_a_structure_root = () => application.Structure.Root.ShouldNotBeNull();
        It should_have_a_structure_with_a_root_of_type_Feature = () => application.Structure.Root.Type.ShouldEqual(typeof(Feature));
        It should_have_a_required_Feature = () => application.Structure.Root.Required.ShouldBeTrue();
        It should_have_a_non_recursive_Feature = () => application.Structure.Root.Recursive.ShouldBeFalse();
        It should_not_have_a_parent = () => application.Structure.Root.HasParent.ShouldBeFalse();
        It should_not_have_children_IApplicationStructureFragments = () => application.Structure.Root.Children.Count().ShouldEqual(0);
    }
}