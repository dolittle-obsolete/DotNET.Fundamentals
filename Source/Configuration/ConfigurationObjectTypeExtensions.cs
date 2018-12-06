/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Reflection;

namespace Dolittle.Configuration
{
    /// <summary>
    /// A set of extension methods for dealing with <see cref="IConfigurationObject"/> type objects
    /// </summary>
    public static class ConfigurationObjectTypeExtensions
    {
        /// <summary>
        /// Get the friendly name - if any - given to the <see cref="IConfigurationObject"/>, using the <see cref="NameAttribute"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get for</param>
        /// <returns>Friendly name, or the type name if none is given</returns>
        public static string GetFriendlyConfigurationName(this Type type)
        {
            if( type.HasAttribute<NameAttribute>() ) return ((NameAttribute)type.GetCustomAttributes(typeof(NameAttribute), false)[0]).Name;
            return type.Name.ToLowerInvariant();
        }
    }
}
