/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.DependencyInversion.Bootstrap
{
    /// <summary>
    /// Exception that gets thrown when there are no implementations of <see cref="ICanProvideContainer"/> loaded
    /// </summary>
    public class MissingContainerProvider : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingContainerProvider"/>
        /// </summary>
        public MissingContainerProvider() : base("There is no provider for an IOC Container - add a reference to an extension that provides this; http://github.com/dolittle-extensions")
        {

        }

    }
}