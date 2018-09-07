/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// The exception that is thrown when an object is not enumerable
    /// </summary>
    public class ObjectIsNotEnumerable : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="ObjectIsNotEnumerable"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ObjectIsNotEnumerable(string message) : base(message)
        {

        }
        
    }
}