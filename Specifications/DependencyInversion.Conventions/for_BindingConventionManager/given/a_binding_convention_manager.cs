using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.DependencyInversion.Conventions.Specs.for_BindingConventionManager.given
{
    public class a_binding_convention_manager
    {
        protected static BindingConventionManager manager;
        protected static Mock<IContainer> container;
        protected static Mock<ITypeFinder> type_finder;

        Establish context = () =>
                                {
                                    container = new Mock<IContainer>();
                                    type_finder = new Mock<ITypeFinder>();
                                    manager = new BindingConventionManager(container.Object, type_finder.Object);
                                };
    }
}
