// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Concepts;
using Dolittle.Security;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common types.
    /// </summary>
    public static class Extensions
    {
        static string _argumentsKey = $"arguments{Metadata.BinaryHeaderSuffix}";

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

        /// <summary>
        /// Convert a <see cref="Execution.ExecutionContext"/> to <see cref="Execution.Contracts.ExecutionContext"/>.
        /// </summary>
        /// <param name="executionContext"><see cref="Execution.ExecutionContext"/> to convert from.</param>
        /// <returns>Converted <see cref="Execution.Contracts.ExecutionContext"/>.</returns>
        public static Execution.Contracts.ExecutionContext ToProtobuf(this Execution.ExecutionContext executionContext)
        {
            var message = new Execution.Contracts.ExecutionContext
            {
                Microservice = executionContext.BoundedContext.ToProtobuf(),
                Tenant = executionContext.Tenant.ToProtobuf(),
                CorrelationId = executionContext.CorrelationId.ToProtobuf(),
            };
            message.Claims.AddRange(executionContext.Claims.ToProtobuf());

            return message;
        }

        /// <summary>
        /// Convert from <see cref="Claims"/> to <see cref="IEnumerable{T}"/> of <see cref="Security.Contracts.Claim"/>.
        /// </summary>
        /// <param name="claims"><see cref="Claims"/> to convert from.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Security.Contracts.Claim"/>.</returns>
        public static IEnumerable<Security.Contracts.Claim> ToProtobuf(this Claims claims)
        {
            return claims.Select(_ => new Security.Contracts.Claim
            {
                Key = _.Name,
                Value = _.Value,
                ValueType = _.ValueType
            });
        }

        /// <summary>
        /// Convert from <see cref="IEnumerable{T}"/> of <see cref="Security.Contracts.Claim"/> to <see cref="Claims"/>.
        /// </summary>
        /// <param name="claims"><see cref="IEnumerable{T}"/> of <see cref="Security.Contracts.Claim"/> to convert from.</param>
        /// <returns>Converted <see cref="Claims"/>.</returns>
        public static Claims ToClaims(this IEnumerable<Security.Contracts.Claim> claims)
        {
            return new Claims(claims.Select(_ => new Claim(_.Key, _.Value, _.ValueType)));
        }

        /// <summary>
        /// Convert to metadata that is used as arguments in the header.
        /// </summary>
        /// <param name="message"><see cref="IMessage"/> to convert.</param>
        /// <returns>A <see cref="Metadata.Entry"/> that can be used directly.</returns>
        public static Metadata.Entry ToArgumentsMetadata(this IMessage message)
        {
            return new Metadata.Entry(_argumentsKey, message.ToByteArray());
        }

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