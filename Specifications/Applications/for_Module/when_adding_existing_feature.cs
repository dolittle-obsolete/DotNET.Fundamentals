using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_Module
{
    public class when_adding_existing_feature
    {
        static Mock<IBoundedContext> bounded_context_mock;
        static Module module;
        static Feature feature;
        static Exception exception;

        Establish context = () =>
        {
            bounded_context_mock = new Mock<IBoundedContext>();
            module = new Module(bounded_context_mock.Object, new ModuleName{Value = "Some Module"});
            feature = new Feature(module, "Feature");
        };

        Because of = () => exception = Catch.Exception(() => module.AddFeature(feature));

        It should_throw_feature_already_added_to_module = () => exception.ShouldBeOfExactType<FeatureAlreadyAddedToModule>();
    }
}
