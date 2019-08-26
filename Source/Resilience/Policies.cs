/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Types;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Represents an implementation of <see cref="IPolicies"/>
    /// </summary>
    /// <remarks>
    /// For each type of policy there can only be one definition. This means there can be only
    /// one defined for each specific type, one for given name or one specifying the default.
    /// </remarks>
    [Singleton]
    public class Policies : IPolicies
    {
        readonly IInstancesOf<ICanDefineDefaultPolicy> _defaultPolicyDefiners;
        readonly IPolicy _defaultPolicy;
        
        /// <summary>
        /// Initializes a new instance of <see cref="Policies"/>
        /// </summary>
        /// <param name="defaultPolicyDefiners">Instances of default policy definers</param>
        public Policies(IInstancesOf<ICanDefineDefaultPolicy> defaultPolicyDefiners) //, IInstancesOf<ICanDefineNamedPolicy> namedPolicyDefiners
        {
            _defaultPolicyDefiners = defaultPolicyDefiners;
            _defaultPolicy = DefineDefaultPolicy();
        }


        /// <inheritdoc/>
        public IPolicy GetDefault()
        {
            return _defaultPolicy;
        }

        /// <inheritdoc/>
        public INamedPolicy GetNamed(string name)
        {
            throw new System.NotImplementedException();
        }


        /// <inheritdoc/>
        public IPolicyFor<T> GetFor<T>()
        {
            throw new System.NotImplementedException();
        }


        IPolicy DefineDefaultPolicy()
        {
            ThrowIfMultipleDefaultPoilicyDefinersAreFound();
            var underlyingPolicy = _defaultPolicyDefiners.FirstOrDefault()?.Define();
            var policy = underlyingPolicy != null ? (IPolicy)new Policy(underlyingPolicy) : new NullPolicy();

            return policy;
        }

        void ThrowIfMultipleDefaultPoilicyDefinersAreFound()
        {
            if (_defaultPolicyDefiners.Count() > 1) throw new MultipleDefaultPolicyDefinersFound();
        }
    }
}