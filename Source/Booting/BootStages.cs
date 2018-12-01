/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Booting.Stages;
using Dolittle.Collections;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.Booting
{
    /// <summary>
    /// Represents an implementation of <see cref="IBootStages"/>
    /// </summary>
    public class BootStages : IBootStages
    {
        readonly IEnumerable<ICanPerformPartOfBootStage> _initialFixedStages;

        readonly Queue<ICanPerformPartOfBootStage> _stages;

        /// <summary>
        /// Initializes a new instance of <see cref="BootStages"/>
        /// </summary>
        public BootStages()
        {
            _initialFixedStages = new ICanPerformPartOfBootStage[]
            {
                new InitialSystem(),
                new Discovery(),
                new PostDiscovery(DiscoverBootStages)
            };
            _stages = new Queue<ICanPerformPartOfBootStage>(_initialFixedStages);
        }

        /// <inheritdoc/>
        public IEnumerable<BootStageResult> Perform(Boot boot)
        {
            var results = new List<BootStageResult>();
            var associations = new Dictionary<string, object>();

            while (_stages.Count > 0)
            {
                var stage = _stages.Dequeue();
                var performer = stage.GetType().GetInterfaces().SingleOrDefault(_ => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(ICanPerformPartOfBootStage<>));
                var settingsType = performer.GetGenericArguments() [0];
                var settings = boot.GetSettingsByType(settingsType);
                var method = performer.GetMethod("Perform", BindingFlags.Public | BindingFlags.Instance);
                var builder = new BootStageBuilder(associations);
                method.Invoke(stage, new object[] { settings, builder });
                var result = builder.Build();
                results.Add(result);
                
                // TODO: MAKE IMMUTABLE - WE SHOULD BE SEEING ONLY THE ASSOCIATIONS FROM THE STAGE - NOT ALL IN THE RESULT
                result.Associations.ForEach(_ => associations[_.Key] = _.Value);
            }

            return results;
        }

        void DiscoverBootStages(ITypeFinder typeFinder)
        {
            var bootStagePerformerTypes = typeFinder.FindMultiple<ICanPerformPartOfBootStage>();
            var bootStagePerformers = bootStagePerformerTypes
                .Where(_ => !_initialFixedStages.Any(existing => existing.GetType() == _))
                .Select(_ =>
                {
                    ThrowIfMissingDefaultConstructorForBootStagePerformer(_);
                    return Activator.CreateInstance(_) as ICanPerformPartOfBootStage;
                });

            bootStagePerformers.ForEach(_ => _stages.Enqueue(_));
        }
        void ThrowIfMissingDefaultConstructorForBootStagePerformer(Type type)
        {
            if (!type.HasDefaultConstructor()) new MissingDefaultConstructorForBootStagePerformer(type);
        }
    }
}