/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructure"/>
    /// </summary>
    public class ApplicationStructure : IApplicationStructure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructure"/>
        /// </summary>
        /// <param name="root"><see cref="IApplicationStructureFragment">Root fragment</see> for the structure</param>
        public ApplicationStructure(IApplicationStructureFragment root)
        {
            Root = root;
        }

        /// <inheritdoc/>
        public IApplicationStructureFragment Root { get; }

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
            return Root.Equals(other.Root);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is IApplicationStructure other)
                return Equals(other);
            return false;
        }

        /// <inheritdoc/>
        public static bool operator ==(ApplicationStructure x, ApplicationStructure y)
        {
            return x.Equals(y);
        }

        /// <inheritdoc/>
        public static bool operator !=(ApplicationStructure x, ApplicationStructure y)
        {
            return !x.Equals(y);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Root.GetHashCode();
        }
        
    }
}