// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;
using Google.Protobuf;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert a <see cref="ByteString"/> to a <see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/>.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="idAsBytes"><see cref="ByteString"/> to convert.</param>
        /// <returns>Converted <see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/>.</returns>
        public static T To<T>(this ByteString idAsBytes)
            where T : ConceptAs<System.Guid>, new()
        {
            return new T { Value = new System.Guid(idAsBytes.ToByteArray()) };
        }

        /// <summary>
        /// Convert a <see cref="ByteString"/> to <see cref="System.Guid"/>.
        /// </summary>
        /// <param name="idAsBytes"><see cref="ByteString"/> to convert.</param>
        /// <returns>Converted <see cref="System.Guid"/>.</returns>
        public static System.Guid ToGuid(this ByteString idAsBytes)
        {
            return new System.Guid(idAsBytes.ToByteArray());
        }

        /// <summary>
        /// Convert a <see cref="System.Guid"/> to <see cref="ByteString"/>.
        /// </summary>
        /// <param name="id"><see cref="System.Guid"/> to convert.</param>
        /// <returns>Converted <see cref="ByteString"/>.</returns>
        public static ByteString ToProtobuf(this System.Guid id)
        {
            return ByteString.CopyFrom(id.ToByteArray());
        }

        /// <summary>
        /// Convert a <see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/> to <see cref="ByteString"/>.
        /// </summary>
        /// <param name="id"><see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/> to convert.</param>
        /// <returns>Converted <see cref="ByteString"/>.</returns>
        public static ByteString ToProtobuf(this ConceptAs<System.Guid> id)
        {
            return ByteString.CopyFrom(id.Value.ToByteArray());
        }
    }
}