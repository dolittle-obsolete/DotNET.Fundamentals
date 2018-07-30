using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when an <see cref="IApplication"/> is created with an invalid <see cref="IApplication"/>
    /// </summary>
    public class InvalidApplication : Exception
    {
        static string _defaultMessage = $"The {typeof(IApplication).FullName} is constructed with an invalid {typeof(IApplication).FullName}.";
         /// <summary>
        /// Initializes a new instance of <see cref="InvalidApplication"/>
        /// </summary>
        public InvalidApplication() 
            : this(_defaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InvalidApplication"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public InvalidApplication(string message) : base(message) 
        {
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidApplication"/>
        /// </summary>
        /// <param name="innerException"></param>
        public InvalidApplication(InvalidApplication innerException) 
            : this(_defaultMessage, innerException)
        {
        }
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidApplication"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InvalidApplication(string message, InvalidApplication innerException) 
            : base(message, innerException)
        {
        }
    }
}