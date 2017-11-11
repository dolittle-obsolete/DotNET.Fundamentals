/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Applications
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
        internal Application(ApplicationName name, IApplicationStructure structure)
        {
            Name = name;
            Structure = structure;
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
    }
}
