/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Exception that gets thrown when missing a <see cref="IValueConverter"/> for a <see cref="Type"/>
    /// </summary>
    public class MissingValueConverter : Exception
    {

        /// <summary>
        /// Initializes a new instance of <see cref="MissingValueConverter"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> that does not have a <see cref="IValueConverter"/></param>
        public MissingValueConverter(Type type) : base($"Missing value converter for type '{type.AssemblyQualifiedName}'") {}
        
    }
}