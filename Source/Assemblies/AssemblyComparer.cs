/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents a comparer for comparing assemblies, typically used in Distinct() 
    /// </summary>
    public class AssemblyComparer : IEqualityComparer<Assembly>
    {
        /// <inheritdoc/>
        public bool Equals(Assembly x, Assembly y)
        {
            return x.FullName == y.FullName;
        }

        /// <inheritdoc/>
        public int GetHashCode(Assembly obj)
        {
            return obj.GetHashCode();
        }
    }
}