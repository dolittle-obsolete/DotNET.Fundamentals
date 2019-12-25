// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Defines how the constructor to be used to build a particular type is identified.
    /// </summary>
    public interface IConstructorProvider
    {
        /// <summary>
        /// Gets a constuctor for the specified type.
        /// </summary>
        /// <param name="type">The type that a constructor is required for.</param>
        /// <returns>A ConstructorInfo that can be used to build an instance of the Type.</returns>
        ConstructorInfo GetFor(Type type);

        /// <summary>
        /// Gets a constuctor for the specified type.
        /// </summary>
        /// <typeparam name="T">The type that a constructor is required for.</typeparam>
        /// <returns>A ConstructorInfo that can be used to build an instance of the Type.</returns>
        ConstructorInfo Get<T>();
    }
}