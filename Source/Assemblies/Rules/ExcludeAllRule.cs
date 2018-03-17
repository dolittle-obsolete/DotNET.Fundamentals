/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Dolittle.Specifications;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies.Rules
{
    /// <summary>
    /// Represents a <see cref="Specification{T}">rule</see> specific to <see cref="Assembly">assemblies</see> 
    /// and used for the <see cref="Assemblies"/>
    /// </summary>
    public class ExcludeAllRule : Specification<Library>
    {
        /// <summary>
        /// Initializes an instance of <see cref="ExcludeAllRule"/>
        /// </summary>
        public ExcludeAllRule()
        {
            Predicate = a => false;
        }
    }
}
