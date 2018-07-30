using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when a <see cref="IApplicationStructure"/> is invalid because it has a <see cref="IApplicationStructureFragment"/> that cannot be recursive is recursive.
    /// </summary>
    public class NonRecursiveStructureFragmentSetAsRecursive : InvalidApplicationStructure
    {
        /// <summary>
        /// Initializes an instance of <see cref="NonRecursiveStructureFragmentSetAsRecursive"/>
        /// </summary>
        /// <param name="structureFragmentType"></param>
        public NonRecursiveStructureFragmentSetAsRecursive(Type structureFragmentType)
        : base ($"The {typeof(IApplicationStructureFragment).FullName} with Type = {structureFragmentType.FullName} cannot be Recursive")
        {
        }
    }
}