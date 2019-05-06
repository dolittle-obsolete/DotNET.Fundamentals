/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Strings;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="IBuildMessages"/>
    /// </summary>
    [Singleton]
    public class BuildMessages : IBuildMessages
    {
        const int IndentSize = 2;
        int _indentLevel = 0;

        /// <inheritdoc/>
        public void Indent()
        {
            _indentLevel++;
        }

        /// <inheritdoc/>
        public void Unindent()
        {
            _indentLevel--;
            if( _indentLevel < 0 ) _indentLevel = 0;
        }
        

        /// <inheritdoc/>
        public void Trace(string message)
        {
            Console.WriteLine(HandleIndentationFor(message).White());
        }

        /// <inheritdoc/>
        public void Error(string message)
        {
            Console.Error.WriteLine(HandleIndentationFor(message).Red());
        }

        /// <inheritdoc/>
        public void Information(string message)
        {
            Console.WriteLine(HandleIndentationFor(message).White());
        }

        /// <inheritdoc/>
        public void Warning(string message)
        {
            Console.WriteLine(HandleIndentationFor(message).Yellow());
        }


        string HandleIndentationFor(string message)
        {
            return $"{new String(' ', IndentSize*_indentLevel)}{message}";
        }
    }
}