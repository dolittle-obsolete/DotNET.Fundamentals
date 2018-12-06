/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.DependencyInversion;

namespace Dolittle.Booting
{
    /// <summary>
    /// Exception that gets thrown when the <see cref="IContainer"/> is not set yet
    /// </summary>
    public class ContainerNotSetYet : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ContainerNotSetYet"/>
        /// </summary>
        public ContainerNotSetYet() : base("Container has not been set yet ") { }
    }
}