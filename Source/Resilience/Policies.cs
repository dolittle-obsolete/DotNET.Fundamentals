/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
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
        readonly IInstancesOf<ICanDefineNamedPolicy> _namedPolicyDefiners;
        readonly IInstancesOf<ICanDefinePolicyForType> _typedPolicyDefiners;
        readonly IDictionary<string, INamedPolicy> _namedPolicies = new Dictionary<string, INamedPolicy>();
        readonly IDictionary<Type, IPolicy> _typedPolicies = new Dictionary<Type, IPolicy>();

        /// <summary>
        /// Initializes a new instance of <see cref="Policies"/>
        /// </summary>
        /// <param name="defaultPolicyDefiners">Instances of <see cref="ICanDefineDefaultPolicy">default policy definers</see></param>
        /// <param name="namedPolicyDefiners">Instances of <see cref="ICanDefineNamedPolicy">named policy definers</see></param>
        /// <param name="typedPolicyDefiners">Instances of <see cref="ICanDefinePolicyForType">typed policy definers</see></param>
        public Policies(
            IInstancesOf<ICanDefineDefaultPolicy> defaultPolicyDefiners,
            IInstancesOf<ICanDefineNamedPolicy> namedPolicyDefiners,
            IInstancesOf<ICanDefinePolicyForType> typedPolicyDefiners)
        {
            _defaultPolicyDefiners = defaultPolicyDefiners;
            _namedPolicyDefiners = namedPolicyDefiners;
            _typedPolicyDefiners = typedPolicyDefiners;
            Default = DefineDefaultPolicy();
            PopulateNamedPolicies();
            PopulateTypedPolicies();
        }

        /// <inheritdoc/>
        public IPolicy Default { get; }

        /// <inheritdoc/>
        public INamedPolicy GetNamed(string name)
        {
            if (_namedPolicies.ContainsKey(name)) return _namedPolicies[name];
            var policy = new NamedPolicy(name, Default);
            _namedPolicies[name] = policy;
            return policy;
        }

        /// <inheritdoc/>
        public IPolicyFor<T> GetFor<T>()
        {
            var type = typeof(T);
            if (_typedPolicies.ContainsKey(type)) return _typedPolicies[type] as IPolicyFor<T>;
            var policyFor = typeof(PolicyFor<>).MakeGenericType(type);

            var constructor = policyFor.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null,
                new Type[]
                {
                    typeof(IPolicy)
                },
                new ParameterModifier[]
                {
                    new ParameterModifier(1)
                });

            var policy = constructor.Invoke(new [] {  Default }) as IPolicyFor<T>;
            _typedPolicies[type] = policy;
            return policy;
        }

        IPolicy DefineDefaultPolicy()
        {
            ThrowIfMultipleDefaultPoilicyDefinersAreFound();
            var underlyingPolicy = _defaultPolicyDefiners.FirstOrDefault()?.Define();
            var policy = underlyingPolicy != null ? (IPolicy) new Policy(underlyingPolicy) : new PassThroughPolicy();

            return policy;
        }

        void PopulateNamedPolicies()
        {
            _namedPolicyDefiners.ForEach(_ =>
            {
                ThrowIfMultiplePolicyForNameFound(_.Name);
                _namedPolicies[_.Name] = new NamedPolicy(_.Name, _.Define());
            });
        }

        void PopulateTypedPolicies()
        {
            _typedPolicyDefiners.ForEach(_ =>
            {
                ThrowIfMultiplePolicyForTypeFound(_.Type);
                var policyFor = typeof(PolicyFor<>).MakeGenericType(_.Type);
                var constructor = policyFor.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null,
                    new Type[]
                    {
                        typeof(Polly.Policy)
                    },
                    new ParameterModifier[]
                    {
                        new ParameterModifier(1)
                    });

                _typedPolicies[_.Type] = constructor.Invoke(new [] {  _.Define() }) as IPolicy;
            });
        }

        void ThrowIfMultipleDefaultPoilicyDefinersAreFound()
        {
            if (_defaultPolicyDefiners.Count() > 1) throw new MultipleDefaultPolicyDefinersFound();
        }

        void ThrowIfMultiplePolicyForNameFound(string name)
        {
            if (_namedPolicies.ContainsKey(name)) throw new MultiplePolicyDefinersForNameFound(name);
        }

        void ThrowIfMultiplePolicyForTypeFound(Type type)
        {
            if (_typedPolicies.ContainsKey(type)) throw new MultiplePolicyDefinersForTypeFound(type);
        }
    }
}