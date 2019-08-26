/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Resilience
{
    /// <summary>
    /// Defines a system that manages all <see cref="IPolicyFor{T}"/>
    /// </summary>
    public interface IPolicies
    {
        /// <summary>
        /// Gets a named policy
        /// </summary>
        /// <param name="name"></param>
        /// <returns><see cref="IPolicy"/> to use</returns>
        /// <remarks>
        /// If there is no policy with the given name, the system will return whatever is 
        /// the default policy. If nothing is <see cref="ICanDefineDefaultPolicy">defining</see>
        /// a default policy, a <see cref="NullPolicy"/> will be returned.
        /// </remarks>
        INamedPolicy GetNamed(string name);
        
        /// <summary>
        /// Get the default policy
        /// </summary>
        /// <returns><see cref="IPolicy"/> to use</returns>
        /// <remarks>
        /// If nothing is <see cref="ICanDefineDefaultPolicy">defining</see>
        /// a default policy, a <see cref="NullPolicy"/> will be returned.
        /// </remarks>
        IPolicy GetDefault();

        /// <summary>
        /// Get policy for a specific type
        /// </summary>
        /// <typeparam name="T">Type to get policy for</typeparam>
        /// <returns><see cref="IPolicy"/> to use</returns>
        /// <remarks>
        /// If there is no policy with the given name, the system will return whatever is 
        /// the default policy. If nothing is <see cref="ICanDefineDefaultPolicy">defining</see>
        /// a default policy, a <see cref="NullPolicy"/> will be returned.
        /// </remarks>
        IPolicyFor<T> GetFor<T>();
    }
}