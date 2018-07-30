using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_Application
{
    public class when_comparing_with_similar_but_different_structures
    {
        
        const string name = "Application";

        static IApplication application_a, application_b;

        Establish context = () =>
        {
            application_a = Application.WithName(name)
                .WithStructureStartingWith<BoundedContext>(b => b.Required
                    .WithChild<Module>(m => m.Required
                        .WithChild<Feature>()))
                .Build(new NullApplicationValidationStrategy());
                
            application_b = Application.WithName(name)
                .WithStructureStartingWith<BoundedContext>(b => b.Required
                    .WithChild<Module>(m => m.Required
                        .WithChild<Feature>(f => f.Required)))
                .Build(new NullApplicationValidationStrategy());

            
        };
        It should_not_be_the_same = () => application_a.Equals(application_b).ShouldBeFalse();
        It should_not_have_same_structure = () => application_a.Structure.Equals(application_b.Structure).ShouldBeFalse();
        It should_have_different_hash_codes => () => application_a.GetHashCode().Equals(application_b.GetHashCode()).ShouldBeFalse();
    }
}