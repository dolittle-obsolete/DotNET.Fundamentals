﻿using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_SubFeature
{
    public class when_adding_sub_feature
    {
        static Mock<IFeature> parent_feature_mock;
        static SubFeature feature;
        static ISubFeature sub_feature;

        Establish context = () =>
        {
            parent_feature_mock = new Mock<IFeature>();
            feature = new SubFeature(parent_feature_mock.Object, new FeatureName {Value = "Some feature"});
            sub_feature = new SubFeature(feature, "Sub Feature");
        };
        
        It should_contain_the_sub_feature = () => feature.Children.ShouldContainOnly(sub_feature);
    }
}