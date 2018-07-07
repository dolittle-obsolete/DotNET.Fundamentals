using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_BoundedContext
{
    public class when_adding_feature
    {
        static BoundedContext bounded_context;
        static Mock<IFeature> feature;

        Establish context = () =>
        {
            bounded_context = new BoundedContext(new BoundedContextName{Value = "Some bounded context"});
            feature = new Mock<IFeature>();
        };

        Because of = () => bounded_context.AddFeature(feature.Object);

        It should_add_the_feature = () => bounded_context.Children.ShouldContain(feature.Object);
    }
}
