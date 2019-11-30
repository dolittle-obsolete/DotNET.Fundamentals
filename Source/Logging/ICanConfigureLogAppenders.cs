// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Logging
{
    /// <summary>
    /// Defines a convention for configuring <see cref="ILogAppenders"/>.
    /// </summary>
    public interface ICanConfigureLogAppenders
    {
        /// <summary>
        /// Configure <see cref="ILogAppenders"/>.
        /// </summary>
        /// <param name="logAppenders"><see cref="ILogAppenders"/> to configure.</param>
        void Configure(ILogAppenders logAppenders);
    }
}
