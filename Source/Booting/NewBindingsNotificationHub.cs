/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.DependencyInversion;

namespace Dolittle.Booting
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanNotifyForNewBindings"/>
    /// </summary>
    public class NewBindingsNotificationHub : ICanNotifyForNewBindings
    {
        event Action<IBindingCollection>    _subscribers = (_) => {};

        /// <inheritdoc/>
        public void Notify(IBindingCollection bindings)
        {
            _subscribers(bindings);
        }

        /// <inheritdoc/>
        public void SubscribeTo(Action<IBindingCollection> subscriber)
        {
            _subscribers += subscriber;
        }
    }
}
