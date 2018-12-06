/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Booting
{
    /// <summary>
    /// Represents the exception tat gets thrown when an association is missing
    /// </summary>
    public class MissingAssociation : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingAssociation"/>
        /// </summary>
        /// <param name="key">Key for association that is missing</param>
        public MissingAssociation(string key) : base($"Missing association with key '{key}'") {}
    }
}