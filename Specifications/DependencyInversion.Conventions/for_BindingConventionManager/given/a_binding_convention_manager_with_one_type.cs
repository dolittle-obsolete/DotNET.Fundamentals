using System;
using Machine.Specifications;

namespace doLittle.DependencyInversion.Conventions.Specs.for_BindingConventionManager.given
{
    public class a_binding_convention_manager_with_one_type : a_binding_convention_manager
    {
        protected static Type service_type;

        Establish context = () =>
                                {
                                    service_type = typeof (IService);
                                    type_finder.SetupGet(t => t.All).Returns(new[] {service_type});
                                };
    }
}