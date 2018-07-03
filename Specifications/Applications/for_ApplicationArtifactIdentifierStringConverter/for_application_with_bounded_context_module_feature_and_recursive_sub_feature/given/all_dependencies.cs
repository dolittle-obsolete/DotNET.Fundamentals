using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifierStringConverter.for_application_with_bounded_context_module_feature_and_recursive_sub_feature.given
{
    public class all_dependencies
    {
        protected const string application_name = "MyApplication";
        protected static IApplication application; 

        Establish context = () => 
            application = 
                Application.WithName(application_name)
                    .WithStructureStartingWith<BoundedContext>(boundedContextBuilder => 
                        boundedContextBuilder.Required
                        .WithChild<Module>(moduleBuilder => 
                            moduleBuilder.Required
                            .WithChild<Feature>(featureBuilder => 
                                featureBuilder.Required.WithChild<SubFeature>(subFeatureBuilder =>
                                subFeatureBuilder.Recursive))))
                .Build();
    }
}