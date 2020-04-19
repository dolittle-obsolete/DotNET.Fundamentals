// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using grpc = contracts::Dolittle.Protobuf.Contracts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents a failure.
    /// </summary>
    public class Failure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        /// <param name="id"><see cref="FailureId" />.</param>
        /// <param name="reason"><see cref="FailureReason" />.</param>
        public Failure(FailureId id, FailureReason reason)
        {
            Id = id;
            Reason = reason;
        }

        /// <summary>
        /// Gets the <see cref="FailureId" />.
        /// </summary>
        public FailureId Id { get; }

        /// <summary>
        /// Gets the <see cref="FailureReason" />.
        /// </summary>
        public FailureReason Reason { get; }

        /// <summary>
        /// Implicitly convert <see cref="Failure" /> to <see cref="grpc.Failure" />.
        /// </summary>
        /// <param name="failure"><see cref="Failure" /> to convert.</param>
        public static implicit operator grpc.Failure(Failure failure) => new grpc.Failure { Id = failure.Id.ToProtobuf(), Reason = failure.Reason };

        /// <summary>
        /// Implicitly convert <see cref="grpc.Failure" /> to <see cref="Failure" />.
        /// </summary>
        /// <param name="failure"><see cref="grpc.Failure" /> to convert.</param>
        public static implicit operator Failure(grpc.Failure failure) => failure.ToFailure();
    }
}