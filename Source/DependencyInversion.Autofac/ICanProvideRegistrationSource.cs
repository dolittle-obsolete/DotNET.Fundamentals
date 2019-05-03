/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Autofac.Core;

namespace Dolittle.DependencyInversion.Autofac
{
    /// <summary>
    /// Defines a provider of <see cref="IRegistrationSource"/> implementations
    /// </summary>
    public interface ICanProvideRegistrationSources
    {
        /// <summary>
        /// Method that gets called for providing <see cref="IRegistrationSource">registration sources</see>
        /// </summary>
        /// <returns></returns>
        IEnumerable<IRegistrationSource> Provide();
    }
}