/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.for_ConfigurationObjectProvider
{
    public class when_asking_if_can_provide_for_configuration_object_type_used_by_performer : given.all_dependencies
    {
        class config_object : IConfigurationObject {}

        class performer : ICanPerformPostBuildTasks
        {
            public performer(config_object configObject) {}

            public void Perform() {}
        }

        static ConfigurationObjectProvider provider;
        static bool result;

        Establish context = () =>
        {
            type_finder.Setup(_ => _.FindMultiple<ICanPerformPostBuildTasks>()).Returns(new[] {
                typeof(performer)
            });

            provider = new ConfigurationObjectProvider(type_finder.Object, get_container);
        };

        Because of = () => result = provider.CanProvide(typeof(config_object));

        It should_be_able_to_provide = () => result.ShouldBeTrue();

    }

}