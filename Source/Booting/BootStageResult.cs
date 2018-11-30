/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.DependencyInversion;

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Represents the result of a <see cref="BootStage"/>
    /// </summary>
    public class BootStageResult
    {
        /// <summary>
        /// Gets the Container to use from the <see cref="BootStage"/> and on
        /// </summary>
        public IContainer Container { get; }

        /// <summary>
        /// Gets the <see cref="IBindingCollection">bindings</see> built from the stage
        /// </summary>
        /// <value></value>
        public IBindingCollection Bindings { get; }

        /// <summary>
        /// Gets any associations of type vs instance
        /// </summary>
        public IReadOnlyDictionary<Type, object> Associations { get; }
    }
}