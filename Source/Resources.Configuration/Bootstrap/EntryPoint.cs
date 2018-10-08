/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Types;

namespace Dolittle.Resources.Configuration.Bootstrap
{
    /// <summary>
    /// Represents the entrypoint for bootstrapping the Resource System
    /// </summary>
    public class EntryPoint
    {
        /// <summary>
        /// Initializes the Resource System 
        /// </summary>
        /// <param name="typeFinder"></param>
        public static void Initialize(ITypeFinder typeFinder)
        {
            ResourceSystemBindings.TypeFinder = typeFinder;
        } 
    }
}