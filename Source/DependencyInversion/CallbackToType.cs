/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// An abstraction for a callback that returns a <see cref="Type"/>
    /// </summary>
    public class CallbackToType
    {
        /// <summary>
        /// Creates an instance of <see cref="CallbackToType"/>
        /// </summary>
        /// <param name="callback"></param>
        public static CallbackToType Create(Func<Type> callback) => new CallbackToType(callback);

        /// <summary>
        /// The callback
        /// </summary>
        /// <value></value>
        public Func<Type> Callback {get;}

        CallbackToType(Func<Type> callback)
        {
            Callback = callback;
        } 
    }
}