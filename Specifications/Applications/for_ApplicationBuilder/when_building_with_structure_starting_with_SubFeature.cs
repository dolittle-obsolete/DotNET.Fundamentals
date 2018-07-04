using Machine.Specifications;
using System.Linq;
namespace Dolittle.Applications.Specs.for_ApplicationBuilder
{
    public class when_building_with_structure_starting_with_SubFeature : given.an_ApplicationBuilder
    {
        static IApplicationStructureFragment root;

        Because of = () => 
            root = application_builder
                .WithStructureStartingWith<SubFeature>(b => 
                    b.Required)
            .Build().Structure.Root;
        It should_have_a_structure_root = () => root.ShouldNotBeNull();
        It should_have_a_structure_with_a_root_of_type_SubFeature = () => root.Type.ShouldEqual(typeof(SubFeature));
        It should_have_a_required_SubFeature  = () => root.Required.ShouldBeTrue();
        It should_have_a_non_recursive_SubFeature  = () => root.Recursive.ShouldBeFalse();
        It should_not_have_a_parent = () => root.HasParent.ShouldBeFalse();
        It should_not_have_children_IApplicationStructureFragments = () => root.Children.Count().ShouldEqual(0);
    }
}