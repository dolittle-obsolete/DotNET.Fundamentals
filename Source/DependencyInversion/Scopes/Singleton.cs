/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.DependencyInversion.Scopes
{
    /// <summary>
    /// Represents a <see cref="IScope"/> for singleton - one instance per process
    /// Adhering to the highlander principle; there can be only one
    /// </summary>
    public class Singleton : IScope
    { 
    }
}