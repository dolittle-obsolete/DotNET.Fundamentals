/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Assemblies;

namespace Dolittle.Types
{
    /// <summary>
    /// Defines a system that is capable of feeding types from <see cref="IAssemblies"/> to
    /// <see cref="IContractToImplementorsMap"/>
    /// </summary>
    public interface ITypeFeeder
    {
        /// <summary>
        /// Feed types from <see cref="IAssemblies"/> to <see cref="IContractToImplementorsMap"/>
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> to feed types from</param>
        /// <param name="map"><see cref="IContractToImplementorsMap"/> to feed to</param>
        void Feed(IAssemblies assemblies, IContractToImplementorsMap map);
    }
}
