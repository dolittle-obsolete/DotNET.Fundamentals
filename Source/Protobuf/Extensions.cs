/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Google.Protobuf;
using Dolittle.Concepts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common types
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert a <see cref="ByteString"/> to a <see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/>
        /// </summary>
        /// <param name="guid"><see cref="ByteString"/> to convert</param>
        /// <returns>Converted <see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/></returns>
        public static T To<T>(this ByteString guid) where T : ConceptAs<System.Guid>, new()
        {
            return new T { Value = new System.Guid(guid.ToByteArray()) };
        }

        /// <summary>
        /// Convert a <see cref="ByteString"/> to <see cref="System.Guid"/>
        /// </summary>
        /// <param name="guid"><see cref="ByteString"/> to convert</param>
        /// <returns>Converted <see cref="System.Guid"/></returns>
        public static System.Guid ToGuid(this ByteString guid)
        {
            return new System.Guid(guid.ToByteArray());
        }

        /// <summary>
        /// Convert a <see cref="System.Guid"/> to <see cref="ByteString"/>
        /// </summary>
        /// <param name="guid"><see cref="System.Guid"/> to convert</param>
        /// <returns>Converted <see cref="ByteString"/></returns>
        public static ByteString ToProtobuf(this System.Guid guid)
        {
            var converted = ByteString.CopyFrom(guid.ToByteArray());
            return converted;
        }

        /// <summary>
        /// Convert a <see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/> to <see cref="ByteString"/>
        /// </summary>
        /// <param name="guid"><see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/> to convert</param>
        /// <returns>Converted <see cref="ByteString"/></returns>
        public static ByteString ToProtobuf(this ConceptAs<System.Guid> guid)
        {
            var converted = ByteString.CopyFrom(guid.Value.ToByteArray());
            return converted;
        }
    }
}