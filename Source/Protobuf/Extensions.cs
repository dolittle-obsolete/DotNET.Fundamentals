// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Linq;
using System.Reflection;
using Dolittle.Concepts;
using Google.Protobuf;
using Grpc.Core;
using grpc = contracts::Dolittle.Protobuf.Contracts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common types.
    /// </summary>
    public static class Extensions
    {
        static readonly string _argumentsKey = $"arguments{Metadata.BinaryHeaderSuffix}";

        /// <summary>
        /// Convert a <see cref="ByteString"/> to a <see cref="ConceptAs{T}"/> of type <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="id"><see cref="grpc.Uuid"/> to convert.</param>
        /// <returns>Converted <see cref="ConceptAs{T}"/> of type <see cref="Guid"/>.</returns>
        public static T To<T>(this grpc.Uuid id)
            where T : ConceptAs<System.Guid>, new() => new T { Value = new Guid(id.Value.ToByteArray()) };

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
        public static grpc.Uuid ToProtobuf(this ConceptAs<Guid> id) =>
            new grpc.Uuid { Value = ByteString.CopyFrom(id.Value.ToByteArray()) };

        /// <summary>
        /// Convert a <see cref="ConceptAs{T}" /> <see cref="string" /> to <see cref="grpc.Failure" /> with not set <see cref="Guid" /> as failure id.
        /// </summary>
        /// <param name="failureReason">The reason for failure.</param>
        /// <returns>Converted <see cref="grpc.Failure" />.</returns>
        public static grpc.Failure ToProtobufFailure(this ConceptAs<string> failureReason) => failureReason.Value.ToProtobufFailure(Guid.Empty);

        /// <summary>
        /// Convert a <see cref="ConceptAs{T}" /> <see cref="string" /> to <see cref="grpc.Failure" /> with given <see cref="Guid" /> as failure id.
        /// </summary>
        /// <param name="failureReason">The reason for failure.</param>
        /// <param name="failureId">The failure id.</param>
        /// <returns>Converted <see cref="grpc.Failure" />.</returns>
        public static grpc.Failure ToProtobufFailure(this ConceptAs<string> failureReason, Guid failureId) => failureReason.Value.ToProtobufFailure(failureId);

        /// <summary>
        /// Convert a <see cref="string" /> to <see cref="grpc.Failure" /> with not set <see cref="Guid" /> as failure id.
        /// </summary>
        /// <param name="failureReason">The reason for failure.</param>
        /// <returns>Converted <see cref="grpc.Failure" />.</returns>
        public static grpc.Failure ToProtobufFailure(this string failureReason) => failureReason.ToProtobufFailure(Guid.Empty);

        /// <summary>
        /// Convert a <see cref="string" /> to <see cref="grpc.Failure" /> with given <see cref="Guid" /> as failure id.
        /// </summary>
        /// <param name="failureReason">The reason for failure.</param>
        /// <param name="failureId">The failure id.</param>
        /// <returns>Converted <see cref="grpc.Failure" />.</returns>
        public static grpc.Failure ToProtobufFailure(this string failureReason, Guid failureId) =>
            new grpc.Failure { Reason = failureReason, Id = failureId.ToProtobuf() };

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

        /// <summary>
        /// Convert to metadata that is used as arguments in the header.
        /// </summary>
        /// <param name="message"><see cref="IMessage"/> to convert.</param>
        /// <returns>A <see cref="Metadata.Entry"/> that can be used directly.</returns>
        public static Metadata.Entry ToArgumentsMetadata(this IMessage message) =>
            new Metadata.Entry(_argumentsKey, message.ToByteArray());

        /// <summary>
        /// Get the arguments message from a <see cref="ServerCallContext"/>.
        /// </summary>
        /// <typeparam name="TMessage">The type of <see cref="IMessage"/>.</typeparam>
        /// <param name="callContext"><see cref="ServerCallContext"/> to get arguments message from.</param>
        /// <exceptions><see cref="MissingArgumentsHeaderOnCallContext"/>.</exceptions>
        /// <returns>The arguments message instance.</returns>
        public static TMessage GetArgumentsMessage<TMessage>(this ServerCallContext callContext)
            where TMessage : IMessage<TMessage>, new()
        {
            var entry = callContext.RequestHeaders.FirstOrDefault(_ => _.Key == _argumentsKey);
            if (entry == default)
            {
                throw new MissingArgumentsHeaderOnCallContext(callContext);
            }

            var property = typeof(TMessage).GetProperty("Parser", BindingFlags.Public | BindingFlags.Static);
            if (property == default)
            {
                throw new MissingParserForMessageType(typeof(TMessage));
            }

            var parser = property.GetValue(null) as MessageParser<TMessage>;
            return parser.ParseFrom(entry.ValueBytes);
        }
    }
}