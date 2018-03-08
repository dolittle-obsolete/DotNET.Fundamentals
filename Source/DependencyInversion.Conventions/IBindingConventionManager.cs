/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.DependencyInversion.Conventions
{
    /// <summary>
    /// Defines a manager for binding conventions
    /// </summary>
    public interface IBindingConventionManager
    {
        /// <summary>
        /// Discover bindings and initialize
        /// </summary>
        IBindingCollection DiscoverAndSetupBindings();
    }
}
