using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.DependencyInversion.Conventions.Specs.for_BindingConventionManager
{
    public class when_initializing_with_one_convention_and_one_type : given.a_binding_convention_manager_with_one_type
    {
        static Mock<IBindingConvention> convention_mock;
        static Type convention_type;

        Establish context = () =>
                                {
                                    convention_mock = new Mock<IBindingConvention>();
                                    convention_type = convention_mock.Object.GetType();
                                    manager.Add(convention_type);
                                    container.Setup(c => c.Get(convention_type)).Returns(convention_mock.Object);
                                    convention_mock.Setup(c => c.CanResolve(container.Object, service_type)).Returns(true);
                                };

        Because of = () => manager.Initialize();

        It should_resolve = () => convention_mock.Verify(c => c.Resolve(container.Object, service_type));
    }
}