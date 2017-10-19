/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using doLittle.Execution;
using doLittle.Types;

namespace doLittle.Security
{
    /// <summary>
    /// Represents an implementation of <see cref="ISecurityManager"/>
    /// </summary>
    [Singleton]
    public class SecurityManager : ISecurityManager
    {
        readonly IEnumerable<ISecurityDescriptor> _securityDescriptors;

        /// <summary>
        /// Initializes a new instance of <see cref="SecurityManager"/>
        /// </summary>
        /// <param name="securityDescriptors"><see cref="IInstancesOf{ISecurityDescriptor}">Instances of security descriptors</see></param>
        public SecurityManager(IInstancesOf<ISecurityDescriptor> securityDescriptors)
        {
            _securityDescriptors = securityDescriptors;
        }

        /// <inheritdoc/>
        public AuthorizationResult Authorize<T>(object target) where T : ISecurityAction
        {
            var result = new AuthorizationResult();
            if (!_securityDescriptors.Any())
                return result;

            var applicableSecurityDescriptors = _securityDescriptors.Where(sd => sd.CanAuthorize<T>(target));

            if (!applicableSecurityDescriptors.Any())
                return result;

            foreach(var securityDescriptor in applicableSecurityDescriptors)
                result.ProcessAuthorizeDescriptorResult(securityDescriptor.Authorize(target));

            return result;
        }
    }
}
