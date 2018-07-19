using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when an <see cref="Application"/> is created with an invalid <see cref="IApplicationStructure"/>
    /// </summary>
    public class InvalidApplicationStructure : Exception
    {
        static string _defaultMessage = $"The {typeof(Application).FullName} is constructed with an invalid {typeof(IApplicationStructure).FullName}.";
        
        /// Initializes an instance of <see cref="InvalidApplicationStructure"/>
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public InvalidApplicationStructure() 
            : this(_defaultMessage)
        {}
         /// Initializes an instance of <see cref="InvalidApplicationStructure"/>
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public InvalidApplicationStructure(string message) 
            : base(message)
        {}
        /// <summary>
        /// Initializes an instance of <see cref="InvalidApplicationStructure"/>
        /// </summary>
        public InvalidApplicationStructure(InvalidApplicationStructure innerException) 
            : this(_defaultMessage, innerException)
        {}
        /// <summary>
        /// Initializes an instance of <see cref="InvalidApplicationStructure"/>
        /// </summary>
        public InvalidApplicationStructure(string message, InvalidApplicationStructure innerException) 
            : base(message, innerException)
        {}
    }
}