// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents the failure id.
    /// </summary>
    public class FailureId : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly converts the <see cref="Guid" /> to <see cref="FailureId" />.
        /// </summary>
        /// <param name="id"><see cref="Guid" /> to convert.</param>
        public static implicit operator FailureId(Guid id) => new FailureId { Value = id };
    }
}
