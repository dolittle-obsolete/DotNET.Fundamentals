/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.DependencyInversion
{
    /// <summary>
    /// Scope for activation
    /// </summary>
    public enum BindingLifecycle
    {
        /// <summary>
        /// Scoped as a singleton
        /// </summary>
        Singleton,

        /// <summary>
        /// Scoped to transient lifecycle
        /// </summary>
        Transient,

        /// <summary>
        /// Scoped to per thread 
        /// </summary>
        Thread
    }
}