/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplication"/>
    /// </summary>
    public class Application : IApplication
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Application"/>
        /// </summary>
        /// <param name="name"><see cref="ApplicationName">Name</see> of the <see cref="IApplication"/></param>
        /// <param name="structure"><see cref="IApplicationStructure">Structure</see> of the <see cref="IApplication"/></param>
        /// <param name="prefixes"></param>
        internal Application(
            ApplicationName name,
            IApplicationStructure structure,
            IEnumerable<IApplicationLocationSegment> prefixes = null)
        {
            Name = name;
            Structure = structure;
            Prefixes = prefixes ?? new IApplicationLocationSegment[0];
        }

        /// <summary>
        /// Define the <see cref="ApplicationName">name</see> for the <see cref="IApplication"/>
        /// </summary>
        /// <param name="name"><see cref="ApplicationName">Name</see> of the <see cref="IApplication"/></param>
        /// <returns><see cref="IApplicationBuilder"/> to continue building</returns>
        public static IApplicationBuilder WithName(ApplicationName name)
        {
            return new ApplicationBuilder(name);
        }

        /// <inheritdoc/>
        public ApplicationName Name { get; }

        /// <inheritdoc/>
        public IApplicationStructure Structure { get; }

        /// <inheritdoc/>
        public IEnumerable<IApplicationLocationSegment> Prefixes { get; }

        //TODO: Override GetHashCode?
    }
}
