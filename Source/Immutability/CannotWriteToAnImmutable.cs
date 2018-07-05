/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Immutability
{
    /// <summary>
    /// The exception that is thrown when an object is read only and one is writing to it
    /// </summary>
    public class CannotWriteToAnImmutable : ArgumentException
    {
    }
}
