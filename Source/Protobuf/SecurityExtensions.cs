// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Collections.Generic;
using System.Linq;
using Dolittle.Security;
using grpc = contracts::Dolittle.Security.Contracts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common security types.
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Convert from <see cref="Claims"/> to <see cref="IEnumerable{T}"/> of <see cref="grpc.Claim"/>.
        /// </summary>
        /// <param name="claims"><see cref="Claims"/> to convert from.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="grpc.Claim"/>.</returns>
        public static IEnumerable<grpc.Claim> ToProtobuf(this Claims claims) =>
            claims.Select(_ => new grpc.Claim
            {
                Key = _.Name,
                Value = _.Value,
                ValueType = _.ValueType
            });

        /// <summary>
        /// Convert from <see cref="IEnumerable{T}"/> of <see cref="grpc.Claim"/> to <see cref="Claims"/>.
        /// </summary>
        /// <param name="claims"><see cref="IEnumerable{T}"/> of <see cref="grpc.Claim"/> to convert from.</param>
        /// <returns>Converted <see cref="Claims"/>.</returns>
        public static Claims ToClaims(this IEnumerable<grpc.Claim> claims) =>
            new Claims(claims.Select(_ => new Claim(_.Key, _.Value, _.ValueType)));
    }
}