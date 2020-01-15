// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// A delegate representing the capabilitiy of getting the <see cref="IContainer"/> instance.
    /// </summary>
    /// <returns>The <see cref="IContainer"/> instance.</returns>
    public delegate IContainer GetContainer();
}