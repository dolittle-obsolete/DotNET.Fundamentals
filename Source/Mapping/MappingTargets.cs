// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IMappingTargets"/>.
    /// </summary>
    public class MappingTargets : IMappingTargets
    {
        static readonly DefaultMappingTarget _defaultMappingTarget = new DefaultMappingTarget();
        readonly IInstancesOf<IMappingTarget> _mappingTargets;
        readonly Dictionary<Type, IMappingTarget> _mappingTargetsByType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingTargets"/> class.
        /// </summary>
        /// <param name="mappingTargets"><see cref="IInstancesOf{T}"/> of <see cref="IMappingTarget"/>.</param>
        public MappingTargets(IInstancesOf<IMappingTarget> mappingTargets)
        {
            _mappingTargets = mappingTargets;
            _mappingTargetsByType = new Dictionary<Type, IMappingTarget>();
            PopulateMappingTargetsByType();
        }

        /// <inheritdoc/>
        public IMappingTarget GetFor(Type type)
        {
            if (_mappingTargetsByType.ContainsKey(type)) return _mappingTargetsByType[type];

            return _defaultMappingTarget;
        }

        void PopulateMappingTargetsByType()
        {
            _mappingTargets.ForEach(mt => _mappingTargetsByType[mt.TargetType] = mt);
        }
    }
}
