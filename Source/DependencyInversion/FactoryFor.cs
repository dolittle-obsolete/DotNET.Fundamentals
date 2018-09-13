/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// A delegate representing something that can create instances, typically by leveraging the <see cref="IContainer"/>
    /// </summary>
    /// <typeparam name="T">Type to be factory for</typeparam>
    /// <returns>Instance of the type asked for</returns>
    public delegate T FactoryFor<T>();
}