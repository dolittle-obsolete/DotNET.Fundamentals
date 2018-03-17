/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Defines a utility to work with <see cref="Assembly"/>
    /// </summary>
    public interface IAssemblyUtility
    {
        /// <summary>
        /// Check if file is an actual .NET assembly or not
        /// </summary>
        /// <param name="library"><see cref="Library"/> to check</param>
        /// <returns>True if the file is an <see cref="Assembly"/>, false if not</returns>
        bool IsAssembly(Library library);

        /// <summary>
        /// Check if an <see cref="Assembly"/> is dynamic
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <remarks>
        /// The need for this is questionable - the interface <see cref="Assembly"/> does not have the IsDynamic
        /// property as the implementation <see cref="Assembly"/> has. This might go away as there has been
        /// a realization that <see cref="Assembly"/> might not be needed, it was introduced to do testing
        /// easier. Turns out however that the implementation <see cref="Assembly"/> has pretty much everything
        /// virtual.
        /// </remarks>
        bool IsDynamic(Assembly assembly);
    }
}
