using System;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Defines a factory for constructing an instance of a type using a <see cref="PropertyBag" />
    /// </summary>
    public interface ITypeFactory
    {
        /// <summary>
        /// Indicates whether the factory can build the specified type
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>true if it can build, false otherwise</returns>
        bool CanBuild(Type type);
        /// <summary>
        /// Indicates whether the factory can build the specified type
        /// </summary>
        /// <typeparam name="T">The type to check</typeparam>
        /// <returns>true if it can build, false otherwise</returns>
        bool CanBuild<T>();

        /// <summary>
        /// Builds the specific type
        /// </summary>
        /// <param name="type">The type to build</param>
        /// <param name="objectFactory">An instance of <see cref="IObjectFactory" /> to help with building any child types</param>
        /// <param name="source">The instance of <see cref="PropertyBag" /> that is used to populate the instance</param>
        /// <returns>An instance of the type, populated from the <see cref="PropertyBag" /> as an object</returns>
        object Build(Type type, IObjectFactory objectFactory, PropertyBag source);

        /// <summary>
        /// Builds the specific type
        /// </summary>
        /// <typeparam name="T">The type to build</typeparam>
        /// <param name="objectFactory">An instance of <see cref="IObjectFactory" /> to help with building any child types</param>
        /// <param name="source">The instance of <see cref="PropertyBag" /> that is used to populate the instance</param>
        /// <returns>An instance of the type, populated from the <see cref="PropertyBag" /></returns>
        T Build<T>(IObjectFactory objectFactory, PropertyBag source);
    }
}