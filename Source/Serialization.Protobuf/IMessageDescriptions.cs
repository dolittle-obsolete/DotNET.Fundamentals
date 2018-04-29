/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Defines a system for keeping track of <see cref="MessageDescription"/> instances
    /// </summary>
    public interface IMessageDescriptions
    {
        /// <summary>
        /// Check if there is a <see cref="MessageDescription"/> for a specific type or not
        /// </summary>
        /// <typeparam name="T">Type to check for</typeparam>
        /// <returns>True if there is, false if not</returns>
        bool HasFor<T>();

        /// <summary>
        /// Gets a <see cref="MessageDescription"/> for a specific type - if none exist, it will return a default one
        /// </summary>
        /// <typeparam name="T">Type to get for</typeparam>
        /// <returns></returns>
        MessageDescription GetFor<T>();

        /// <summary>
        /// Set a <see cref="MessageDescription"/> for a specific type
        /// </summary>
        /// <typeparam name="T">Type to set for</typeparam>
        void SetFor<T>(MessageDescription description);
    }
}