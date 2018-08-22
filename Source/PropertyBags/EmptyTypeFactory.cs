/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    using System.Collections.Concurrent;
    using Dolittle.Execution;
    using Dolittle.Reflection;

    /// <summary>
    /// Creates a instance of the immutable type using the default ctor
    /// </summary>
    [Singleton]
    public class EmptyTypeFactory : ITypeFactory
    {
        /// <inheritdoc />
        public object Build(Type type, IObjectFactory objectFactory, PropertyBag source)
        {
           return Activator.CreateInstance(type);
        }

        /// <inheritdoc />
        public T Build<T>(IObjectFactory objectFactory, PropertyBag source)
        {
            return (T)Build(typeof(T),objectFactory,source);
        }

        /// <inheritdoc />
        public bool CanBuild(Type type)
        {
            return !type.HasNonDefaultConstructor() && type.HasDefaultConstructor() && !type.HasVisibleProperties();
        }

        /// <inheritdoc />
        public bool CanBuild<T>()
        {
            return CanBuild(typeof(T));
        }
    }
}