/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Applications
{
    /// <summary>
    /// Null representation of <see cref="NullApplicationStructure"/>
    /// </summary>
    public class NullApplicationStructure : IApplicationStructure
    {
        /// <inheritdoc/>
        public IApplicationStructureFragment Root => NullApplicationStructureFragment.Instance;

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        /// <inheritdoc/>
        public int CompareTo(IApplicationStructure other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }

        /// <inheritdoc/>
        public bool Equals(IApplicationStructure other)
        {
            return other is NullApplicationStructure;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is NullApplicationStructure other)
            {
                return Equals(other);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return typeof(NullApplicationStructure).GetHashCode() 
                * typeof(NullApplicationStructure).GetHashCode();
        }
    }
}