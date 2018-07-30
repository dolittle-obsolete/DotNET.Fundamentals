using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when a <see cref="IApplicationStructure"/> is invalid because it has a <see cref="IApplicationStructureFragment"/> with more than one child.
    /// </summary>
    public class ApplicationStructureFragmentMustNotHaveMoreThanOneChild : InvalidApplicationStructure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureFragmentMustNotHaveMoreThanOneChild"/>
        /// </summary>
        /// <param name="structureFragmentType"></param>
        /// <returns></returns>
        public ApplicationStructureFragmentMustNotHaveMoreThanOneChild(Type structureFragmentType) 
        : base($"The {typeof(IApplicationStructureFragment).FullName} with Type = {structureFragmentType.FullName} has more than one children fragments")
        {
        }
    }
}