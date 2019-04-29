/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Lifecycle;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="IBuildMessages"/>
    /// </summary>
    [Singleton]
    public class BuildMessages : IBuildMessages
    {
        /// <inheritdoc/>
        public void Error(string message)
        {
            Console.Error.WriteLine(message);
        }

        /// <inheritdoc/>
        public void Information(string message)
        {
            Console.WriteLine(message);
        }

        /// <inheritdoc/>
        public void Warning(string message)
        {
            Console.WriteLine(message);
        }
    }
}