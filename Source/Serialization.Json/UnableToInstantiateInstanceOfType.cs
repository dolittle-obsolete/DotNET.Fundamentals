namespace Dolittle.Serialization.Json
{
    using System;
    using System.Runtime.Serialization;
    
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class UnableToInstantiateInstanceOfType : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the UnableToInstantiateInstanceOfType custom exception
        /// </summary>
        public UnableToInstantiateInstanceOfType(Type type) 
            : this($"Cannot instantiate type {type?.FullName ?? "[NULL]"}.  Ensure that the type has a default constructor or that there is a matching constructor with the ctor parameters matching the property names.")
        {}
    
        /// <summary>
        ///     Initializes a new instance of the UnableToInstantiateInstanceOfType custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public UnableToInstantiateInstanceOfType(string message)
            : base(message)
        {}
    
        /// <summary>
        ///     Initializes a new instance of the UnableToInstantiateInstanceOfType custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        /// <param name="innerException">An inner exception that is the original source of the error</param>
        public UnableToInstantiateInstanceOfType(string message, Exception innerException)
            : base(message, innerException)
        {}
    
        /// <summary>
        ///     Initializes a new instance of the UnableToInstantiateInstanceOfType custom exception
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
        protected UnableToInstantiateInstanceOfType(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}
    }
}