/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Mono.Cecil;

namespace Dolittle.Build
{
    /// <summary>
    /// Defines a modifier that is capable of performing modifications to an assembly through its
    /// <see cref="AssemblyDefinition"/>
    /// </summary>
    public interface ICanModifyTargetAssembly
    {
        /// <summary>
        /// Modify the <see cref="AssemblyDefinition">assembly</see>
        /// </summary>
        /// <param name="assemblyDefinition"><see cref="AssemblyDefinition">Assembly</see> to modify</param>
        void Modify(AssemblyDefinition assemblyDefinition);
    }
}