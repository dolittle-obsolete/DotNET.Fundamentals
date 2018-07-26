using System;

namespace Dolittle.PropertyBags
{
    public interface ITypeFactory
    {
        bool CanBuild(Type type);
        bool CanBuild<T>();
    }
}