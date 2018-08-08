/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the exceptional situation where a type has more than one <see cref="ITypeFactory" /> to build it
    /// </summary>
    [Serializable]
    public class MultipleFactoriesForType : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the MultipleFactoriesForType custom exception
        /// </summary>
        public MultipleFactoriesForType()
        {}

        /// <summary>
        ///     Initializes a new instance of the MultipleFactoriesForType custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public MultipleFactoriesForType(string message)
            : base(message)
        {}

        /// <summary>
        ///     Initializes a new instance of the MultipleFactoriesForType custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        /// <param name="innerException">An inner exception that is the original source of the error</param>
        public MultipleFactoriesForType(string message, Exception innerException)
            : base(message, innerException)
        {}

        /// <summary>
        ///     Initializes a new instance of the MultipleFactoriesForType custom exception
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
        protected MultipleFactoriesForType(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}
    }
}