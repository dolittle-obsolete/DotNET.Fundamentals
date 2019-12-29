// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IMappingTarget"/> representing the default behavior for mapping to a target.
    /// </summary>
    public class DefaultMappingTarget : IMappingTarget
    {
        /// <inheritdoc/>
        public Type TargetType => typeof(object);

        /// <inheritdoc/>
        public void SetValue(object target, MemberInfo member, object value)
        {
        }
    }
}
