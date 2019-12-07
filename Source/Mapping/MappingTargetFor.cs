// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Represents an abstract implementation of <see cref="IMappingTargetFor{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of object the target is for.</typeparam>
    public abstract class MappingTargetFor<T> : IMappingTargetFor<T>
    {
        /// <inheritdoc/>
        public Type TargetType => typeof(T);

        /// <inheritdoc/>
        public void SetValue(object target, MemberInfo member, object value)
        {
            SetValue((T)target, member, value);
        }

        /// <summary>
        /// Set value for a member with a given value to the specific instance of the type.
        /// </summary>
        /// <param name="target">Target object to set value for.</param>
        /// <param name="member"><see cref="MemberInfo"/> to set for.</param>
        /// <param name="value">Actual value to set.</param>
        protected abstract void SetValue(T target, MemberInfo member, object value);
    }
}
