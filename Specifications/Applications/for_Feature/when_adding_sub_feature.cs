using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_Feature
{
    public class when_adding_sub_feature
    {
        static Mock<IModule> module_mock;
        static Feature feature;
        static SubFeature sub_feature;

        Establish context = () =>
        {
            module_mock = new Mock<IModule>();
            feature = new Feature(module_mock.Object, "Some feature");
            sub_feature = new SubFeature(feature, "Sub Feature");
        };
        
        It should_contain_the_sub_feature = () => feature.Children.ShouldContainOnly(sub_feature);
    }
}
