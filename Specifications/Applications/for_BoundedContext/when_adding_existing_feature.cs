using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_BoundedContext
{
    public class when_adding_existing_feature
    {
        static BoundedContext bounded_context;
        static Mock<IFeature> feature;
        static Exception exception;

        Establish context = () =>
        {
            bounded_context = new BoundedContext("Some bounded context");
            feature = new Mock<IFeature>();
            bounded_context.AddFeature(feature.Object);
        };

        Because of = () => exception = Catch.Exception(() => bounded_context.AddFeature(feature.Object));

        It should_throw_feature_already_added_to_bounded_context = () => exception.ShouldBeOfExactType<FeatureAlreadyAddedToBoundedContext>();
    }
}
