/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// A delegate representing the capabilitiy of getting the <see cref="IContainer"/> instance
    /// </summary>
    /// <returns><see cref="IContainer"/></returns>
    public delegate IContainer GetContainer();
}