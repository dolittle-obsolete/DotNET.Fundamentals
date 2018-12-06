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
using Dolittle.DependencyInversion;
using Dolittle.Logging;
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
        IContainer _container = null;

        /// <summary>
        /// Initializes a new instance of <see cref="BootStages"/>
        /// </summary>
        public BootStages()
        {
            _initialFixedStages = new ICanPerformPartOfBootStage[]
            {
                new Basics(),
                new Stages.Logging(),
                new InitialSystem(),
                new Discovery(),
                new PostDiscovery(DiscoverBootStages)
            };
            _stages = new Queue<ICanPerformPartOfBootStage>(_initialFixedStages);
        }

        ILogger _logger;

        /// <inheritdoc/>
        public BootStagesResult Perform(Boot boot)
        {
            var newBindingsNotificationHub = new NewBindingsNotificationHub();
            var results = new List<BootStageResult>();
            var aggregatedAssociations = new Dictionary<string, object>() {
                { WellKnownAssociations.NewBindingsNotificationHub, newBindingsNotificationHub }
            };
            IBindingCollection bindingCollection = new BindingCollection(new [] {
                new BindingBuilder(Binding.For(typeof(GetContainer))).To((GetContainer)(() => _container)).Build()
            });
            _logger = new NullLogger();

            aggregatedAssociations[WellKnownAssociations.Bindings] = bindingCollection;

            while (_stages.Count > 0)
            {
                var stage = _stages.Dequeue();
                if (aggregatedAssociations.ContainsKey(WellKnownAssociations.Logger)) _logger = aggregatedAssociations[WellKnownAssociations.Logger] as ILogger;

                var interfaces = stage.GetType().GetInterfaces();

                var isBefore = interfaces.Any(_ => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(ICanRunBeforeBootStage<>));
                var isAfter = interfaces.Any(_ => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(ICanRunAfterBootStage<>));

                var suffix = string.Empty;
                if (isBefore) suffix = " (before)";
                if (isAfter) suffix = " (after)";

                _logger.Information($"<********* BOOTSTAGE : {stage.BootStage}{suffix} *********>");

                var performer = interfaces.SingleOrDefault(_ => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(ICanPerformPartOfBootStage<>));
                var settingsType = performer.GetGenericArguments() [0];
                var settings = boot.GetSettingsByType(settingsType);
                var method = performer.GetMethod("Perform", BindingFlags.Public | BindingFlags.Instance);

                aggregatedAssociations[WellKnownAssociations.Bindings] = bindingCollection;
                var builder = new BootStageBuilder(container: _container, initialAssociations: aggregatedAssociations);
                method.Invoke(stage, new object[] { settings, builder });
                var result = builder.Build();
                results.Add(result);

                result.Associations.ForEach(_ => aggregatedAssociations[_.Key] = _.Value);

                newBindingsNotificationHub.Notify(result.Bindings);
                bindingCollection = aggregatedAssociations[WellKnownAssociations.Bindings] as IBindingCollection;

                bindingCollection = new BindingCollection(bindingCollection, result.Bindings);
                _container = result.Container;
            }

            var bootStagesResult = new BootStagesResult(_container, aggregatedAssociations, results);
            return bootStagesResult;
        }

        void DiscoverBootStages(ITypeFinder typeFinder)
        {
            var bootStagePerformerTypes = typeFinder.FindMultiple<ICanPerformPartOfBootStage>();
            var bootStagePerformers = bootStagePerformerTypes
                .Where(_ => !_initialFixedStages.Any(existing => existing.GetType() == _))
                .Select(_ =>
                {
                    ThrowIfMissingDefaultConstructorForBootStagePerformer(_);
                    if (_container != null) return _container.Get(_) as ICanPerformPartOfBootStage;
                    return Activator.CreateInstance(_) as ICanPerformPartOfBootStage;
                })
                .OrderBy(_ => _.BootStage);

            bootStagePerformers.GroupBy(performer => performer.BootStage).ForEach(performers =>
            {
                var beforePerformers = performers.Where(_ => HasInterface(_, typeof(ICanRunBeforeBootStage<>)));
                beforePerformers.ForEach(_stages.Enqueue);

                var performer = performers.Single(_ => HasInterface(_, typeof(ICanPerformBootStage<>)));
                _stages.Enqueue(performer);

                var afterPerformers = performers.Where(_ => HasInterface(_, typeof(ICanRunAfterBootStage<>)));
                afterPerformers.ForEach(_stages.Enqueue);
            });

            ThrowIfMissingBootStage(bootStagePerformers);
        }
        void ThrowIfMissingDefaultConstructorForBootStagePerformer(Type type)
        {
            if (!type.HasDefaultConstructor()) new MissingDefaultConstructorForBootStagePerformer(type);
        }

        void ThrowIfMissingBootStage(IEnumerable<ICanPerformPartOfBootStage> performers)
        {
            var bootStageValues = Enum
                .GetValues(typeof(BootStage))
                .Cast<BootStage>()
                .Where(_ => !_initialFixedStages.Any(existing => existing.BootStage == _));

            bootStageValues.ForEach(bootStage => 
            {
                var hasPerformer = performers.Any(performer =>
                    {
                        var interfaces = performer.GetType().GetInterfaces();

                        var isBefore = interfaces.Any(_ => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(ICanRunBeforeBootStage<>));
                        var isAfter = interfaces.Any(_ => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(ICanRunAfterBootStage<>));
                        if( !isBefore && !isAfter ) return performer.BootStage == bootStage;
                        return false;
                    });
                if( !hasPerformer ) throw new MissingBootStage(bootStage);
            });
        }

        bool HasInterface(ICanPerformPartOfBootStage performer, Type interfaceType)
        {
            var interfaces = performer.GetType().GetInterfaces();
            return interfaces.Any(_ => _.IsGenericType && _.GetGenericTypeDefinition() == interfaceType);
        }
    }
}