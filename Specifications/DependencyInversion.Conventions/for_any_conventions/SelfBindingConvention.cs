using System;

namespace doLittle.DependencyInversion.Conventions.Specs.for_any_conventions
{
    public class SelfBindingConvention : BindingConvention
    {
        public override bool CanResolve(IContainer container, Type service)
        {
            return true;
        }

        public override void Resolve(IContainer container, Type service)
        {
            container.Bind(service, service, GetScopeForTarget(service));
        }
    }
}
