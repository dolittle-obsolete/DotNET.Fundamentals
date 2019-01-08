using System;
using System.Runtime.Serialization;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MissingProperty : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the MissingProperty custom exception
        /// </summary>
        public MissingProperty()
        {}

        /// <summary>
        ///     Initializes a new instance of the MissingProperty custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public MissingProperty(string message)
            : base(message)
        {}

        /// <summary>
        ///     Initializes a new instance of the MissingProperty custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        /// <param name="innerException">An inner exception that is the original source of the error</param>
        public MissingProperty(string message, Exception innerException)
            : base(message, innerException)
        {}

        /// <summary>
        ///     Initializes a new instance of the MissingProperty custom exception
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
        protected MissingProperty(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}
    }
}


