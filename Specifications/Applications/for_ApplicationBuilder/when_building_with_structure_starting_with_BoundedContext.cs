using Machine.Specifications;
using System.Linq;
namespace Dolittle.Applications.Specs.for_ApplicationBuilder
{
    public class when_building_with_structure_starting_with_BoundedContext : given.an_ApplicationBuilder
    {
        static IApplicationStructureFragment root;

        Because of = () => 
            root = application_builder
                .WithStructureStartingWith<BoundedContext>(b => 
                    b.Required)
            .Build().Structure.Root;
        It should_have_a_structure_root = () => root.ShouldNotBeNull();
        It should_have_a_structure_with_a_root_of_type_BoundedContext = () => root.Type.ShouldEqual(typeof(BoundedContext));
        It should_have_a_required_BoundedContext = () => root.Required.ShouldBeTrue();
        It should_have_a_non_recursive_BoundedContext = () => root.Recursive.ShouldBeFalse();
        It should_not_have_a_parent = () => root.HasParent.ShouldBeFalse();
        It should_not_have_children_IApplicationStructureFragments = () => root.Children.Count().ShouldEqual(0);
    }
}