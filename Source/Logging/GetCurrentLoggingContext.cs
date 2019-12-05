// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Logging
{
    /// <summary>
    /// Defines a delegate which retrieves the current <see cref="LoggingContext"/>.
    /// </summary>
    /// <returns>An instance of the current <see cref="LoggingContext"/>.</returns>
    public delegate LoggingContext GetCurrentLoggingContext();
}