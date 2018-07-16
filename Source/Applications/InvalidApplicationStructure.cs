using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when an <see cref="Application"/> is created with an invalid <see cref="IApplicationStructure"/>
    /// </summary>
    public class InvalidApplicationStructure : Exception
    {
        /// <summary>
        /// Initializes an instance of <see cref="InvalidApplicationStructure"/>
        /// </summary>
        /// <returns></returns>
        public InvalidApplicationStructure() 
            : base($"The {typeof(Application).AssemblyQualifiedName} is constructed with an invalid {typeof(IApplicationStructure).AssemblyQualifiedName}.")
        {}
        /// <summary>
        /// Initializes an instance of <see cref="InvalidApplicationStructure"/>
        /// </summary>
        /// <param name="innerException"></param>
        public InvalidApplicationStructure(InvalidApplicationStructure innerException) 
            : base($"The {typeof(Application).AssemblyQualifiedName} is constructed with an invalid {typeof(IApplicationStructure).AssemblyQualifiedName}.", innerException)
        {}
    }
}