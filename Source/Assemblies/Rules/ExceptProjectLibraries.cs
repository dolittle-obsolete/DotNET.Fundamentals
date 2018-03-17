/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Specifications;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies.Rules
{
    /// <summary>
    /// Rule representing an exception for <see cref="IncludeAllRule"/>, 
    /// excluding assembies starting with
    /// </summary>
    public class ExceptProjectLibraries : Specification<Library>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ExceptProjectLibraries"/>
        /// </summary>
        public ExceptProjectLibraries() => Predicate = library => library.Type.ToLowerInvariant() == "project";
    }
}
