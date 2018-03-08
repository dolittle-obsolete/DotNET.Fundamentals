/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.DependencyInversion.Scopes
{
    /// <summary>
    /// Represents a <see cref="IScope"/> for transient - meaning that there will be a new instance
    /// for every activation
    /// </summary>
    public class Transient : IScope
    {
    }
}