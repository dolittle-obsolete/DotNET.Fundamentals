using System.Linq;
using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationBuilder
{
    public class when_building_with_structure_starting_with_required_BoundedContext_required_Module_Feature_and_recursive_SubFeature
        : given.an_ApplicationBuilder
    {
        static IApplicationStructureFragment root;

        Because of = () => {
            root = application_builder
                .WithStructureStartingWith<BoundedContext>(b => 
                    b.Required.WithChild<Module>(m =>
                        m.Required.WithChild<Feature>(f => 
                            f.WithChild<SubFeature>(sf =>
                                sf.Recursive))))
            .Build().Structure.Root;

        };

        // Structure root
        It should_have_a_root_of_type_BoundedContext = () => root.Type.ShouldEqual(typeof(BoundedContext));
        It should_have_a_root_with_1_child = () => root.Children.Count().ShouldEqual(1);
        It should_have_a_required_BoundedContext = () => root.Required.ShouldBeTrue();
        It should_have_a_non_recursive_BoundedContext = () => root.Recursive.ShouldBeFalse();
        It should_have_a_root_without_a_parent = () => root.HasParent.ShouldBeFalse();
        
        // Structure first child 
        It should_have_a_first_child_of_type_Module = () => root.Children.ToArray()[0].Type.ShouldEqual(typeof(Module));
        It should_have_a_first_child_with_1_child = () => root.Children.ToArray()[0].Children.Count().ShouldEqual(1);
        It should_have_a_required_Module = () => root.Children.ToArray()[0].Required.ShouldBeTrue();
        It should_have_a_non_recursive_Module = () => root.Children.ToArray()[0].Recursive.ShouldBeFalse();
        It should_have_a_first_child_with_a_parent = () => root.Children.ToArray()[0].HasParent.ShouldBeTrue();

        // Structure second child 
        It should_have_a_second_child_of_type_Feature = () => root.Children.ToArray()[0].Children.ToArray()[0].Type.ShouldEqual(typeof(Feature));
        It should_have_a_second_child_with_1_child = () => root.Children.ToArray()[0].Children.ToArray()[0].Children.Count().ShouldEqual(1);
        It should_not_have_a_required_Feature = () => root.Children.ToArray()[0].Children.ToArray()[0].Required.ShouldBeFalse();
        It should_have_a_non_recursive_Feature = () => root.Children.ToArray()[0].Children.ToArray()[0].Recursive.ShouldBeFalse();
        It should_have_a_second_child_with_a_parent = () => root.Children.ToArray()[0].Children.ToArray()[0].HasParent.ShouldBeTrue();


        // Structure third child 
        It should_have_a_third_child_of_type_SubFeature = () => root.Children.ToArray()[0].Children.ToArray()[0].Children.ToArray()[0].Type.ShouldEqual(typeof(SubFeature));
        It should_have_a_third_child_with_no_children = () => root.Children.ToArray()[0].Children.ToArray()[0].Children.ToArray()[0].Children.Count().ShouldEqual(0);
        It should_not_have_a_required_SubFeature = () => root.Children.ToArray()[0].Children.ToArray()[0].Children.ToArray()[0].Required.ShouldBeFalse();
        It should_have_a_recursive_SubFeature = () => root.Children.ToArray()[0].Children.ToArray()[0].Children.ToArray()[0].Recursive.ShouldBeTrue();
        It should_have_a_third_child_with_a_parent = () => root.Children.ToArray()[0].Children.ToArray()[0].Children.ToArray()[0].HasParent.ShouldBeTrue();
    }
}