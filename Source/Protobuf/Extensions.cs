// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
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
        /// Convert a <see cref="ByteString"/> to a <see cref="ConceptAs{T}"/> of type <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="id"><see cref="Contracts.Uuid"/> to convert.</param>
        /// <returns>Converted <see cref="ConceptAs{T}"/> of type <see cref="Guid"/>.</returns>
        public static T To<T>(this Contracts.Uuid id)
            where T : ConceptAs<Guid>, new() => new T { Value = id.ToGuid() };

        /// <summary>
        /// Convert a <see cref="Contracts.Uuid"/> to <see cref="Guid"/>.
        /// </summary>
        /// <param name="id"><see cref="Contracts.Uuid"/> to convert.</param>
        /// <returns>Converted <see cref="Guid"/>.</returns>
        public static Guid ToGuid(this Contracts.Uuid id) => new Guid(id.Value.ToByteArray());

        /// <summary>
        /// Convert a <see cref="Guid"/> to <see cref="Contracts.Uuid"/>.
        /// </summary>
        /// <param name="id"><see cref="Guid"/> to convert.</param>
        /// <returns>Converted <see cref="Contracts.Uuid"/>.</returns>
        public static Contracts.Uuid ToProtobuf(this Guid id) =>
            new Contracts.Uuid { Value = ByteString.CopyFrom(id.ToByteArray()) };

        /// <summary>
        /// Convert a <see cref="ConceptAs{T}"/> of type <see cref="Guid"/> to <see cref="Contracts.Uuid"/>.
        /// </summary>
        /// <param name="id"><see cref="ConceptAs{T}"/> of type <see cref="Guid"/> to convert.</param>
        /// <returns>Converted <see cref="Contracts.Uuid"/>.</returns>
        public static Contracts.Uuid ToProtobuf(this ConceptAs<Guid> id) => id.Value.ToProtobuf();

        /// <summary>
        /// Convert a <see cref="Failure" /> to <see cref="Contracts.Failure" />.
        /// </summary>
        /// <param name="failure"><see cref="Failure" /> to convert.</param>
        /// <returns>Converted <see cref="Contracts.Failure" />.</returns>
        public static Contracts.Failure ToProtobuf(this Failure failure) =>
            new Contracts.Failure { Id = failure.Id.ToProtobuf(), Reason = failure.Reason };

        /// <summary>
        /// Convert a <see cref="Contracts.Failure" /> to <see cref="Failure" />.
        /// </summary>
        /// <param name="failure"><see cref="Contracts.Failure" /> to convert.</param>
        /// <returns>Converted <see cref="Failure" />.</returns>
        public static Failure ToFailure(this Contracts.Failure failure) =>
            new Failure(failure.Id.To<FailureId>(), failure.Reason);
    }
}
