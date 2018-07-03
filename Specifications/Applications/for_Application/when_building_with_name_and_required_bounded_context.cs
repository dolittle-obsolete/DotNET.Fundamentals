using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_Application
{
    public class when_building_with_name_and_required_bounded_context
    {
        const string application_name = "Application";

        static IApplication application;

        static bool have_same_name;
        static bool structure_root_type_is_BoundedContext;

        Establish context = () => 
        {
            application = Application.WithName(application_name)
                .WithStructureStartingWith<BoundedContext>(b => b.Required).Build();
        };

        Because of = () => 
        {
            have_same_name = application.Name == application_name;

            structure_root_type_is_BoundedContext = application.Structure.Root.Type == typeof(BoundedContext);
        };

        It should_have_same_application_name = () => have_same_name.ShouldBeTrue();
        It should_have_structure_root_of_type_BoundedContext = () => structure_root_type_is_BoundedContext.ShouldBeTrue();
    }
}