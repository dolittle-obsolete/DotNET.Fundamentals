using System;

namespace doLittle.DependencyInversion.Conventions.Specs.for_BindingConventionManager
{
    public class FakeBindingConvention : IBindingConvention
    {
        public bool CanResolveCalled = false;
        public bool CanResolveProperty = false;
        public bool CanResolve(IContainer container, Type service)
        {
            CanResolveCalled = true;
            return CanResolveProperty;
        }


        public bool ResolveCalled = false;
        public void Resolve(IContainer container, Type service)
        {
            ResolveCalled = true;
        }
    }
}