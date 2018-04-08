using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_SubFeature
{
    public class when_creating
    {
        const string name = "Sub Feature";
        static Mock<IModule> module_mock;
        static Mock<IFeature> parent_feature_mock;
        static SubFeature sub_feature;

        Establish context = () =>
        {
            module_mock = new Mock<IModule>();
            parent_feature_mock = new Mock<IFeature>();
        };

        Because of = () => sub_feature = new SubFeature(parent_feature_mock.Object, name);

        It should_set_the_name = () => ((string) sub_feature.Name).ShouldEqual(name);
        It should_set_the_parent = () => sub_feature.Parent.ShouldEqual(parent_feature_mock.Object);
        It should_add_itself_to_the_parent = () => parent_feature_mock.Verify(p => p.AddSubFeature(sub_feature), Times.Once());
    }
}
