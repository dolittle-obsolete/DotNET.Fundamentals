/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Specifications;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies.Rules
{
    /// <summary>
    /// Represents a <see cref="Specification{T}">rule</see> specific to <see cref="Library">libraries</see> to filter on specific name starting with
    /// </summary>
    public class NameStartsWith : Specification<Library>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NameStartsWith"/> rule
        /// </summary>
        /// <param name="name">Name to check if <see cref="Library"/> starts with</param>
        public NameStartsWith(string name) => Predicate = library => library.Name.StartsWith(name);
    }
}
