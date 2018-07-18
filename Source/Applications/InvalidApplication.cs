using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when an <see cref="IApplication"/> is created with an invalid <see cref="IApplication"/>
    /// </summary>
    public class InvalidApplication : Exception
    {
         /// <summary>
        /// Initializes a new instance of <see cref="InvalidApplication"/>
        /// </summary>
        public InvalidApplication() 
            : base($"The {typeof(IApplication).AssemblyQualifiedName} is constructed with an invalid {typeof(IApplication).AssemblyQualifiedName}.")
        {
        }
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidApplication"/>
        /// </summary>
        /// <param name="innerException"></param>
        public InvalidApplication(InvalidApplication innerException) 
            : base($"The {typeof(IApplication).AssemblyQualifiedName} is constructed with an invalid {typeof(IApplication).AssemblyQualifiedName}.", innerException)
        {
        }
    }
}