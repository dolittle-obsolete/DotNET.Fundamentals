/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Concepts;

namespace doLittle.Applications
{
    /// <summary>
    /// Represents the name of a <see cref="Feature"/>
    /// </summary>
    public class ModuleName : ConceptAs<string>, IApplicationLocationSegmentName
    {
        /// <inheritdoc/>
        public string AsString()
        {
            return this;
        }

        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to a <see cref="ModuleName"/>
        /// </summary>
        /// <param name="moduleName">Name of the module</param>
        public static implicit operator ModuleName(string moduleName)
        {
            return new ModuleName { Value = moduleName };
        }
    }
}
