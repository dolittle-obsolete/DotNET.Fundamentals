/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.DependencyInversion.Scopes
{
    /// <summary>
    /// Represents a <see cref="IScope"/> for singleton per tenant - one instance per process per tenant
    /// Adhering to the highlander principle; there can be only one - per planet. I digress
    /// </summary>
    public class SingletonPerTenant : IScope
    { 
    }
}