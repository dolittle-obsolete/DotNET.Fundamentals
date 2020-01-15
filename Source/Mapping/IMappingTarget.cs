﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Defines a mapping target - more specifically you should look at <see cref="IMappingTargetFor{T}"/>.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface will be automatically registered.
    /// You most likely want to subclass <see cref="MappingTargetFor{T}"/>.
    /// </remarks>
    public interface IMappingTarget
    {
        /// <summary>
        /// Gets the type of target object.
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// Set value for a member with a given value.
        /// </summary>
        /// <param name="target">Target object to set value for.</param>
        /// <param name="member"><see cref="MemberInfo"/> to set for.</param>
        /// <param name="value">Actual value to set.</param>
        void SetValue(object target, MemberInfo member, object value);
    }
}