using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_Feature
{
    public class when_adding_existing_sub_feature
    {
        static Mock<IModule> module_mock;
        static Feature feature;
        static SubFeature sub_feature;
        static Exception exception;

        Establish context = () =>
        {
            module_mock = new Mock<IModule>();
            feature = new Feature(module_mock.Object, "Some feature");
            sub_feature = new SubFeature(feature, "Sub Feature");
        };

        Because of = () => exception = Catch.Exception(() => feature.AddSubFeature(sub_feature));

        It should_throw_sub_feature_already_added_to_feature = () => exception.ShouldBeOfExactType<SubFeatureAlreadyAddedToFeature>();
    }
}
