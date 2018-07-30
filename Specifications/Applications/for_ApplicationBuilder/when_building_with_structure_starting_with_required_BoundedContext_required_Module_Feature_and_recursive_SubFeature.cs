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
            .Build(new NullApplicationValidationStrategy()).Structure.Root;

        };

        // Structure root
        It should_have_a_root_of_type_BoundedContext = () => root.Type.ShouldEqual(typeof(BoundedContext));
        It should_have_a_root_with_1_child = () => root.Children.Count().ShouldEqual(1);
        It should_have_a_required_BoundedContext = () => root.Required.ShouldBeTrue();
        It should_have_a_non_recursive_BoundedContext = () => root.Recursive.ShouldBeFalse();
        It should_have_a_root_without_a_parent = () => root.HasParent.ShouldBeFalse();
        
        // Structure first child 
        It should_have_a_first_child_of_type_Module = () => FirstLevelChild().Type.ShouldEqual(typeof(Module));
        It should_have_a_first_child_with_1_child = () => FirstLevelChild().Children.Count().ShouldEqual(1);
        It should_have_a_required_Module = () => FirstLevelChild().Required.ShouldBeTrue();
        It should_have_a_non_recursive_Module = () => FirstLevelChild().Recursive.ShouldBeFalse();
        It should_have_a_first_child_with_a_parent = () => FirstLevelChild().HasParent.ShouldBeTrue();

        // Structure second child 
        It should_have_a_second_child_of_type_Feature = () => SecondLevelChild().Type.ShouldEqual(typeof(Feature));
        It should_have_a_second_child_with_1_child = () => SecondLevelChild().Children.Count().ShouldEqual(1);
        It should_not_have_a_required_Feature = () => SecondLevelChild().Required.ShouldBeFalse();
        It should_have_a_non_recursive_Feature = () => SecondLevelChild().Recursive.ShouldBeFalse();
        It should_have_a_second_child_with_a_parent = () => SecondLevelChild().HasParent.ShouldBeTrue();


        // Structure third child 
        It should_have_a_third_child_of_type_SubFeature = () => ThirdLevelChild().Type.ShouldEqual(typeof(SubFeature));
        It should_have_a_third_child_with_no_children = () => ThirdLevelChild().Children.Count().ShouldEqual(0);
        It should_not_have_a_required_SubFeature = () => ThirdLevelChild().Required.ShouldBeFalse();
        It should_have_a_recursive_SubFeature = () => ThirdLevelChild().Recursive.ShouldBeTrue();
        It should_have_a_third_child_with_a_parent = () => ThirdLevelChild().HasParent.ShouldBeTrue();
    
        static IApplicationStructureFragment FirstLevelChild()
        {
            return root.Children.ToArray()[0];
        }
        static IApplicationStructureFragment SecondLevelChild()
        {
            return root.Children.ToArray()[0].Children.ToArray()[0];
        }
        static IApplicationStructureFragment ThirdLevelChild()
        {
            return root.Children.ToArray()[0].Children.ToArray()[0].Children.ToArray()[0];
        }
    }
}