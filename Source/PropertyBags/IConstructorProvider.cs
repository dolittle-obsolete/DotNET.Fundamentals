namespace Dolittle.PropertyBags
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines how the constructor to be used to build a particular type is identified
    /// </summary>
    public interface IConstructorProvider
    {
        /// <summary>
        /// Gets a constuctor for the specified type
        /// </summary>
        /// <param name="type">The type that a constructor is required for</param>
        /// <returns>A ConstructorInfo that can be used to build an instance of the Type</returns>
        ConstructorInfo GetFor(Type type);
        /// <summary>
        /// Gets a constuctor for the specified type
        /// </summary>
        /// <typeparam name="type">The type that a constructor is required for</param>
        /// <returns>A ConstructorInfo that can be used to build an instance of the Type</returns>
        ConstructorInfo Get<T>();
    }
}