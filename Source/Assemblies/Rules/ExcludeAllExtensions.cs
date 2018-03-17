/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Specifications;

namespace Dolittle.Assemblies.Rules
{
    /// <summary>
    /// Extensions for <see cref="ExcludeAll"/>
    /// </summary>
    public static class ExcludeAllExtensions
    {
        /// <summary>
        /// Include project libraries
        /// </summary>
        /// <param name="excludeAll"><see cref="ExcludeAll">configuration object</see></param>
        /// <returns>Chain of <see cref="ExcludeAll">configuration object</see></returns>
        public static ExcludeAll ExceptProjectLibraries(this ExcludeAll excludeAll)
        {
            var specification = excludeAll.Specification;

            specification = specification.Or(new ExceptProjectLibraries());

            excludeAll.Specification = specification;
            return excludeAll;
        }

        /// <summary>
        /// Include Dolittle libraries
        /// </summary>
        /// <param name="excludeAll"><see cref="ExcludeAll">configuration object</see></param>
        /// <returns>Chain of <see cref="ExcludeAll">configuration object</see></returns>
        public static ExcludeAll ExceptDolittleLibraries(this ExcludeAll excludeAll)
        {
            var specification = excludeAll.Specification;
            specification = specification.Or(new NameStartsWith("Dolittle"));
            excludeAll.Specification = specification;
            return excludeAll;
        }
    }
}
