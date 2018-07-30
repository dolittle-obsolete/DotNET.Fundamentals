using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when a <see cref="IApplicationStructure"/> is invalid because it the Root <see cref="IApplicationStructureFragment"/> of the <see cref="IApplicationStructure"/>
    /// </summary>
    public class ApplicationStructureMustStartWithABoundedContext : InvalidApplicationStructure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureMustStartWithABoundedContext"/>
        /// </summary>
        /// <returns></returns>
        public ApplicationStructureMustStartWithABoundedContext() 
        : base($"The {typeof(IApplicationStructure).FullName} is built with a structure that does not start with a {typeof(IApplicationStructureFragment).FullName} with Type = {typeof(IBoundedContext).FullName}")
        {
        }
    }
}