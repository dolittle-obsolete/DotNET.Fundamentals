// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.ApplicationModel
{
    /// <summary>
    /// Represents the name of a <see cref="Feature"/>.
    /// </summary>
    public class ModuleName : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to a <see cref="ModuleName"/>.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public static implicit operator ModuleName(string moduleName)
        {
            return new ModuleName { Value = moduleName };
        }
    }
}
