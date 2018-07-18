using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when a <see cref="IApplicationStructure"/> is invalid because it has a <see cref="IApplicationStructureFragment"/> that must be required, but is optional.
    /// </summary>
    public class RequiredStructureFragmentMustBeSetAsRequired : InvalidApplicationStructure
    {
        /// <summary>
        /// Initializes an instance of <see cref="RequiredStructureFragmentMustBeSetAsRequired"/>
        /// </summary>
        /// <param name="structureFragmentType"></param>
        /// <returns></returns>
        public RequiredStructureFragmentMustBeSetAsRequired(Type structureFragmentType) : base()
        {}
    }
}