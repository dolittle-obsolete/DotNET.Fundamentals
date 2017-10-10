/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Configuration
{
    /*
    public class ConfigurationForResourceOf<T>
    {
        public ResourceImplementedBy<T> Implementation { get; }
        public ResourceScope Scope { get; }
    }

    public class ResourceImplementedBy<T>
    {
    }

    public class ResourceScope
    {
    }

    public interface IConfigurationValueResolver
    {
        object Resolve();
    }
    */

    /*

    public class Events : IConfiguration
    {
        public Events()
        {
            Optional<IEventStore>(_ => _.WithFallback<NullEventStore>());
        }
    }

    public interface ICanBuildConfigurationFor<T> where T: IConfiguration
    {
    }

    public static class EventsConfigurationExtensions
    {
        public static ICanBuildConfigurationFor<Events> StoreEventsIn(this ICanBuildConfigurationFor<Events> builder)
        {

        }
    }


    public class EventsConfiguration : ICanConfigure<Events>
    {
        public ICanBuildConfigurationFor<Events> Configure(ICanBuildConfigurationFor<Events> builder)
        {
            builder
                .StoreEventsIn(_ => _.Tables())
                .Lifecycle(_ => _.PerTenant())
        }
    }
    
     */

    /*
         Events: {
             Store: {
                 type: "Tables"  // Alias
                 connectionString: ""
             }
         }


      */
}