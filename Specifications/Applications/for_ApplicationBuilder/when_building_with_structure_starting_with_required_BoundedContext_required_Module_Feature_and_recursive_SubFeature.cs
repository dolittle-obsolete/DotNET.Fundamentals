using System.Linq;
using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationBuilder
{
    public class when_building_with_structure_starting_with_required_BoundedContext_required_Module_Feature_and_recursive_SubFeature
        : given.an_ApplicationBuilder
    {
        static IApplication application;

        Because of = () => {
            application = application_builder
                .WithStructureStartingWith<BoundedContext>(b => 
                    b.Required.WithChild<Module>(m =>
                        m.Required.WithChild<Feature>(f => 
                            f.WithChild<SubFeature>(sf =>
                                sf.Recursive))))
            .Build();

        };

        // Structure root
        It should_have_a_root_of_type_BoundedContext = () => application.Structure.Root.Type.ShouldEqual(typeof(BoundedContext));
        It should_have_a_root_with_1_child = () => application.Structure.Root.Children.Count().ShouldEqual(1);
        It should_have_a_required_BoundedContext = () => application.Structure.Root.Required.ShouldBeTrue();
        It should_have_a_non_recursive_BoundedContext = () => application.Structure.Root.Recursive.ShouldBeFalse();
        It should_have_a_root_without_a_parent = () => application.Structure.Root.HasParent.ShouldBeFalse();
        
        // Structure first child 
        It should_have_a_first_child_of_type_Module = () => application.Structure.Root.Children.ToArray()[0].Type.ShouldEqual(typeof(Module));
        It should_have_a_first_child_with_1_child = () => application.Structure.Root.Children.ToArray()[0].Children.Count().ShouldEqual(1);
        It should_have_a_required_Module = () => application.Structure.Root.Children.ToArray()[0].Required.ShouldBeTrue();
        It should_have_a_non_recursive_Module = () => application.Structure.Root.Children.ToArray()[0].Recursive.ShouldBeFalse();
        It should_have_a_first_child_with_a_parent = () => application.Structure.Root.Children.ToArray()[0].HasParent.ShouldBeTrue();
        It should_have_a_first_child_with_root_as_parent = () => application.Structure.Root.Children.ToArray()[0].Parent.ShouldEqual(application.Structure.Root);

        // Structure second child 
        It should_have_a_second_child_of_type_Feature = () => application.Structure.Root.Children.ToArray()[1].Type.ShouldEqual(typeof(Feature));
        It should_have_a_second_child_with_1_child = () => application.Structure.Root.Children.ToArray()[1].Children.Count().ShouldEqual(1);
        It should_not_have_a_required_Feature = () => application.Structure.Root.Children.ToArray()[1].Required.ShouldBeFalse();
        It should_have_a_non_recursive_Feature = () => application.Structure.Root.Children.ToArray()[1].Recursive.ShouldBeFalse();
        It should_have_a_second_child_with_a_parent = () => application.Structure.Root.Children.ToArray()[1].HasParent.ShouldBeTrue();
        It should_have_a_second_child_with_first_child_as_parent = () => application.Structure.Root.Children.ToArray()[1].Parent.ShouldEqual(application.Structure.Root.Children.ToArray()[0]);


        // Structure third child 
        It should_have_a_third_child_of_type_SubFeature = () => application.Structure.Root.Children.ToArray()[2].Type.ShouldEqual(typeof(SubFeature));
        It should_have_a_third_child_with_no_children = () => application.Structure.Root.Children.ToArray()[2].Children.Count().ShouldEqual(0);
        It should_not_have_a_required_SubFeature = () => application.Structure.Root.Children.ToArray()[2].Required.ShouldBeFalse();
        It should_have_a_recursive_SubFeature = () => application.Structure.Root.Children.ToArray()[2].Recursive.ShouldBeTrue();
        It should_have_a_third_child_that_is_not_a_parent = () => application.Structure.Root.Children.ToArray()[2].HasParent.ShouldBeFalse();
        It should_have_a_third_child_with_second_child_as_parent = () => application.Structure.Root.Children.ToArray()[2].Parent.ShouldEqual(application.Structure.Root.Children.ToArray()[1]);
    }
}