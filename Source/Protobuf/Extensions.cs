// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using Dolittle.Concepts;
using Google.Protobuf;
using grpc = contracts::Dolittle.Protobuf.Contracts;

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
        /// <param name="id"><see cref="grpc.Uuid"/> to convert.</param>
        /// <returns>Converted <see cref="ConceptAs{T}"/> of type <see cref="Guid"/>.</returns>
        public static T To<T>(this grpc.Uuid id)
            where T : ConceptAs<System.Guid>, new() => new T { Value = id.ToGuid() };

        /// <summary>
        /// Convert a <see cref="grpc.Uuid"/> to <see cref="Guid"/>.
        /// </summary>
        /// <param name="id"><see cref="grpc.Uuid"/> to convert.</param>
        /// <returns>Converted <see cref="System.Guid"/>.</returns>
        public static Guid ToGuid(this grpc.Uuid id) => new Guid(id.Value.ToByteArray());

        /// <summary>
        /// Convert a <see cref="Guid"/> to <see cref="grpc.Uuid"/>.
        /// </summary>
        /// <param name="id"><see cref="Guid"/> to convert.</param>
        /// <returns>Converted <see cref="grpc.Uuid"/>.</returns>
        public static grpc.Uuid ToProtobuf(this Guid id) =>
            new grpc.Uuid { Value = ByteString.CopyFrom(id.ToByteArray()) };

        /// <summary>
        /// Convert a <see cref="ConceptAs{T}"/> of type <see cref="Guid"/> to <see cref="grpc.Uuid"/>.
        /// </summary>
        /// <param name="id"><see cref="ConceptAs{T}"/> of type <see cref="Guid"/> to convert.</param>
        /// <returns>Converted <see cref="grpc.Uuid"/>.</returns>
        public static grpc.Uuid ToProtobuf(this ConceptAs<Guid> id) => id.Value.ToProtobuf();

        /// <summary>
        /// Convert a <see cref="Failure" /> to <see cref="grpc.Failure" />.
        /// </summary>
        /// <param name="failure"><see cref="Failure" /> to convert.</param>
        /// <returns>Converted <see cref="grpc.Failure" />.</returns>
        public static grpc.Failure ToProtobuf(this Failure failure) =>
            new grpc.Failure { Id = failure.Id.ToProtobuf(), Reason = failure.Reason };

        /// <summary>
        /// Convert a <see cref="grpc.Failure" /> to <see cref="Failure" />.
        /// </summary>
        /// <param name="failure"><see cref="grpc.Failure" /> to convert.</param>
        /// <returns>Converted <see cref="Failure" />.</returns>
        public static Failure ToFailure(this grpc.Failure failure) =>
            new Failure(failure.Id.To<FailureId>(), failure.Reason);
    }
}
