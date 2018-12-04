/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Booting.Stages;

namespace Dolittle.Booting
{
    /// <summary>
    /// Extensions for building <see cref="DiscoverySettings"/> 
    /// </summary>
    public static class DiscoveryBootBuilderExtensions
    {
        /// <summary>
        /// With a set of known <see cref="AssemblyName">assemblies</see>
        /// </summary>
        /// <param name="bootBuilder"><see cref="BootBuilder"/> to build</param>
        /// <param name="assemblies"><see cref="IEnumerable{T}"/> of <see cref="AssemblyName"/> to use as well known assemblies, instead of discovering</param>
        /// <returns>Chained <see cref="BootBuilder"/></returns>
        public static IBootBuilder WithAssemblies(this IBootBuilder bootBuilder, IEnumerable<AssemblyName> assemblies)
        {
            bootBuilder.Set<DiscoverySettings>(_ => _.AssemblyProvider, new WellKnownAssembliesAssemblyProvider(assemblies));
            return bootBuilder;
        }
    }
}