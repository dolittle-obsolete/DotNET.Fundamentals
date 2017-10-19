/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Security
{
    /// <summary>
    /// Extensions for <see cref="ISecurable"/>
    /// </summary>
    public static class SecurableExtensions
    {
        /// <summary>
        /// Define a user actor for a <see cref="ISecurable">securable</see>
        /// </summary>
        /// <param name="securable"><see cref="ISecurable"/> to secure</param>
        /// <returns>The <see cref="ISecurable"/> chain</returns>
        public static ISecurityActor User(this ISecurable securable)
        {
            throw new NotImplementedException();
        }
    }
}
