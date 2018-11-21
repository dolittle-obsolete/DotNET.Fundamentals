/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Defines an attribute that can be used to define a name of a <see cref="IConfigurationObject"/>
    /// 
    /// This name can be used as the basis of a configuration section or even as a file name. This 
    /// decision is up to each <see cref="ICanProvideConfigurationObjects">provider</see>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NameAttribute"/>
        /// </summary>
        /// <param name="name">Name to use</param>
        public NameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name to be associated with the <see cref="IConfigurationObject"/>
        /// </summary>
        public string Name { get; }
    }
}