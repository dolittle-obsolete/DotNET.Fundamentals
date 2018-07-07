using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_Feature
{
    public class when_creating
    {
        static FeatureName name = new FeatureName{Value = "Some feature"};
        static Mock<IModule> module_mock;
        static Feature feature;

        Because of = () =>
        {
            module_mock = new Mock<IModule>();
            feature = new Feature(module_mock.Object, name);
        };

        It should_set_the_name = () => ((string) feature.Name).ShouldEqual(name.Value);
        It should_set_module = () => feature.Parent.ShouldEqual(module_mock.Object);
        It should_add_the_itself_to_the_module = () => module_mock.Verify(m => m.AddChild(feature), Times.Once());
    }
}
