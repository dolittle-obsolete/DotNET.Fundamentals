/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Dolittle.Assemblies;

namespace Dolittle.Types.Bootstrap
{
    /// <summary>
    /// Represents the entrypoint for starting up and initialization for an app using the Type system
    /// </summary>
    public class Boot
    {
        /// <summary>
        /// Initialize systems needed for the type system and discovery mechanisms to work
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> that will be used</param>
        /// <param name="entryAssembly"><see cref="Assembly"/> to use as entry assembly - null indicates it will ask for the entry assembly</param>
        /// <returns><see cref="ITypeFinder"/> that can be used</returns>
        public static ITypeFinder Start(IAssemblies assemblies, Assembly entryAssembly = null)
        {
            var contractToImplementorsMap = new ContractToImplementorsMap();

            if( entryAssembly == null ) entryAssembly = Assembly.GetEntryAssembly();
            contractToImplementorsMap.Feed(entryAssembly.GetTypes());
            
            var typeFinder = new TypeFinder(assemblies, contractToImplementorsMap);
            return typeFinder;
        }
    }
}