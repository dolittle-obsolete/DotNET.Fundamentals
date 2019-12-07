// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Expresses that the <see cref="ITypeFactory" /> is a user defined factory.
    /// This will take precedence over the built in factories.
    /// </summary>
    /// <typeparam name="T">The type to build.</typeparam>
    public interface IUserDefinedTypeFactory<T> : ITypeFactory
    {
    }
}