using System;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Expresses that the <see cref="ITypeFactory" /> is a user defined factory.
    /// This will take precedence over the built in factories.
    /// </summary>
    /// <typeparam name="T">The type to build.</typeparam>
    public interface IUserDefinedTypeFactory<T> : ITypeFactory
    {

    }
}