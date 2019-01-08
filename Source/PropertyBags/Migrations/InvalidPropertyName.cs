namespace Dolittle.PropertyBags.Migrations
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class InvalidPropertyName : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the InvalidPropertyName custom exception
        /// </summary>
        public InvalidPropertyName()
        {}

        /// <summary>
        ///     Initializes a new instance of the InvalidPropertyName custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public InvalidPropertyName(string message)
            : base(message)
        {}

        /// <summary>
        ///     Initializes a new instance of the InvalidPropertyName custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        /// <param name="innerException">An inner exception that is the original source of the error</param>
        public InvalidPropertyName(string message, Exception innerException)
            : base(message, innerException)
        {}

        /// <summary>
        ///     Initializes a new instance of the InvalidPropertyName custom exception
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
        protected InvalidPropertyName(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}
    }
}
