/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// Represents a build for <see cref="IScope"/> for a <see cref="Binding"/>
    /// </summary>
    public interface IBindingScopeBuilder
    {
        /// <summary>
        /// Sets the scope to Singleton; adhering to the highlander principle - "There can be only one"
        /// </summary>
        /// <remarks>
        /// Important to read the highlander principle with the scottish accent of Sean Connery
        /// </remarks>
        void Singleton();

        /// <summary>
        /// Sets the scope to Singleton per tenant; adhering to the highlander principle - "There can be only one" (PS: per planet - or tenant in this case)
        /// </summary>
        /// <remarks>
        /// Important to read the highlander principle with the scottish accent of Sean Connery
        /// </remarks>
        void SingletonPerTenant();

        /// <summary>
        /// Builds the Binding
        /// </summary>
        /// <returns>The resulting <see cref="Binding"/></returns>
        Binding Build();
    }
}