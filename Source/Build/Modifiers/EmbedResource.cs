/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Mono.Cecil;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a <see cref="ICanModifyTargetAssembly"/> that is capable of embedding resources
    /// into an <see cref="AssemblyDefinition">assembly</see>
    /// </summary>
    public class EmbedResource : ICanModifyTargetAssembly
    {
        private readonly string _name;
        private readonly byte[] _bytes;

        /// <summary>
        /// Initializes a new instance of <see cref="EmbedResource"/>
        /// </summary>
        /// <param name="name">Name of the resource to embed - fully qualified</param>
        /// <param name="bytes">Byte array to embed</param>
        public EmbedResource(string name, byte[] bytes)
        {
            _name = name;
            _bytes = bytes;
        }

        /// <inheritdoc/>
        public void Modify(AssemblyDefinition assemblyDefinition)
        {
            var embeddedResource = new EmbeddedResource(_name, ManifestResourceAttributes.Public, _bytes);
            assemblyDefinition.MainModule.Resources.Add(embeddedResource);
        }
    }
}