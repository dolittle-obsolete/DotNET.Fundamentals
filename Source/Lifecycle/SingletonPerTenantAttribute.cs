// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Lifecycle
{
    /// <summary>
    /// Indicates that a class is Singleton per tenant and should be treated as such
    /// for any factory creating an instance of a class marked with this.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SingletonPerTenantAttribute : Attribute
    {
    }
}