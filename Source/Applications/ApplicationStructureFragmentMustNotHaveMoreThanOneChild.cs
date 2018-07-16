using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when an <see cref="Application"/> is created with an invalid <see cref="IApplicationStructure"/> because it has a <see cref="IApplicationStructureFragment"/> with more than one child.
    /// </summary>
    public class ApplicationStructureFragmentMustNotHaveMoreThanOneChild : InvalidApplicationStructure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureFragmentMustNotHaveMoreThanOneChild"/>
        /// </summary>
        /// <param name="structureFragmentType"></param>
        /// <returns></returns>
        public ApplicationStructureFragmentMustNotHaveMoreThanOneChild(Type structureFragmentType) : base()
        {
        }
    }
}