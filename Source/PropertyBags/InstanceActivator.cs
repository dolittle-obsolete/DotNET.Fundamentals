// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Defines a method for creating an instance using the provided constructor parameters.
    /// </summary>
    /// <param name="args">Parameters to be used in the constructor.</param>
    /// <returns>An instance of an object.</returns>
    public delegate object InstanceActivator(params object[] args);
}