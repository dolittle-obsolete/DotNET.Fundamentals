using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_Module
{
    public class when_adding_feature
    {
        static Mock<IBoundedContext> bounded_context_mock;
        static Module module;
        static Feature feature;

        Establish context = () =>
        {
            bounded_context_mock = new Mock<IBoundedContext>();
            module = new Module(bounded_context_mock.Object, new ModuleName{Value = "Some Module"});
            feature = new Feature(module, "Feature");
        };

        It should_contain_the_feature = () => module.Children.ShouldContainOnly(feature);
    }
}
