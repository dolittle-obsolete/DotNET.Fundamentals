using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_DefaultApplicationStructureValidationStrategy
{
    public class when_building_Application_with_a_standard_ApplicationStructure : given.a_standard_ApplicationBuilder
    {
        protected static IApplicationStructure application_structure;
        
        Establish context = () =>
        {
            application_builder = application_builder
                .WithStructureStartingWith<BoundedContext>(bc => bc.Required
                    .WithChild<Module>(m => m.Required
                        .WithChild<Feature>(f => f
                            .WithChild<SubFeature>(sf => sf.Required))));
        };
        Because of = () => application_structure = application_builder.Build(new given.SimplifiedApplicationValidationStrategy()).Structure;

        It should_have_created_an_ApplicationStructure = () => application_structure.ShouldNotBeNull();
    }
}