/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Execution
{
    /// <summary>
    /// Represents the concept of a runtime environment - e.g. Testing, Development, Staging, Production
    /// </summary>
    public class Environment : ConceptAs<string>
    {
        /// <summary>
        /// Represents an undetermined environment 
        /// </summary>
        public static readonly Environment Undetermined = "Undetermined";

        /// <summary>
        /// Represents a production like environment 
        /// </summary>
        public static readonly Environment Production = "Production";

        /// <summary>
        /// Represents a development like environment
        /// </summary>
        public static readonly Environment Development = "Development";

        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="Environment"/>
        /// </summary>
        public static implicit operator Environment(string environment)
        {
            return new Environment { Value = environment };
        }
    }
}
