/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Indicates the exceptional situation where a type does not have a factory capable of constructing it
    /// </summary>
    [Serializable]
    public class NoFactoriesForType : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the NoFactoriesForType custom exception
        /// </summary>
        public NoFactoriesForType()
        {}

        /// <summary>
        ///     Initializes a new instance of the NoFactoriesForType custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public NoFactoriesForType(string message)
            : base(message)
        {}

        /// <summary>
        ///     Initializes a new instance of the NoFactoriesForType custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        /// <param name="innerException">An inner exception that is the original source of the error</param>
        public NoFactoriesForType(string message, Exception innerException)
            : base(message, innerException)
        {}

        /// <summary>
        ///     Initializes a new instance of the NoFactoriesForType custom exception
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
        protected NoFactoriesForType(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}
    }
}