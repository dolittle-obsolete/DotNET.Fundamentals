/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Configuration.Files
{
    /// <summary>
    /// 
    /// </summary>
    public class ManifestResourceStreamConfigurationObjectsProvider : ICanProvideConfigurationObjects
    {
        /// <inheritdoc/>
        public bool CanProvide(Type type)
        {
            return false;
        }

        /// <inheritdoc/>
        public object Provide(Type type)
        {
            return null;
        }
    }
}