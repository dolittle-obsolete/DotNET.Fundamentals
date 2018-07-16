using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when an <see cref="Application"/> is created with an invalid <see cref="IApplicationStructure"/> because it has a <see cref="IApplicationStructureFragment"/> that must be required, but is optional.
    /// </summary>
    public class RequiredStructureFragmentIsOptional : InvalidApplicationStructure
    {
        /// <summary>
        /// Initializes an instance of <see cref="RequiredStructureFragmentIsOptional"/>
        /// </summary>
        /// <param name="structureFragmentType"></param>
        /// <returns></returns>
        public RequiredStructureFragmentIsOptional(Type structureFragmentType) : base()
        {}
    }
}