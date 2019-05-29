namespace Dolittle.Serialization.Json
{
    using System;
    using System.Runtime.Serialization;
    
    /// <summary>
    /// Indicates when the serializer is unable to instantiate a type.
    /// Typically due to the lack of a default constructor and mismatched parameters in the non-default constructor(s).
    /// </summary>
    [Serializable]
    public class UnableToInstantiateInstanceOfType : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the UnableToInstantiateInstanceOfType custom exception
        /// </summary>
        public UnableToInstantiateInstanceOfType(Type type) 
            : base($"Cannot instantiate type {type?.FullName ?? "[NULL]"}.  Ensure that the type has a default constructor or that there is a matching constructor with the ctor parameters matching the property names.")
        {}
    }
}