/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.Serialization;

namespace Dolittle.PropertyBags
{   
     /// <summary>
     /// The exception that gets thrown when getting the generic Build method of an <see cref="IObjectFactory"/>
     /// </summary>
    public class GenericBuildMethodNotFound : Exception
    {
        /// <summary>
        /// Creates an instance of <see cref="GenericBuildMethodNotFound"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public GenericBuildMethodNotFound(string message) : base(message)
        {
        }
    }
}