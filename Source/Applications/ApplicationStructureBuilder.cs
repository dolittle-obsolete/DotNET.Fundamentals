/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructureBuilder"/>
    /// </summary>
    public class ApplicationStructureBuilder : IApplicationStructureBuilder
    {
        readonly IApplicationStructureFragment _root;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureBuilder"/>
        /// </summary>
        /// <param name="root"><see cref="IApplicationStructureFragment">Root</see> for the structure</param>
        internal ApplicationStructureBuilder(IApplicationStructureFragment root)
        {
            _root = root;
        }

        /// <summary>
        /// Define the root of the <see cref="IApplicationStructure"/>
        /// </summary>
        /// <param name="root">The root <see cref="IApplicationStructureFragment"/></param>
        /// <returns><see cref="IApplicationStructureBuilder"/> to continue building</returns>
        public static IApplicationStructureBuilder WithRoot(IApplicationStructureFragment root)
        {
            var builder = new ApplicationStructureBuilder(root);
            return builder;
        }

        /// <inheritdoc/>
        public IApplicationStructure Build()
        {
            var structure = new ApplicationStructure(_root);
            return structure;
        }
    }
}