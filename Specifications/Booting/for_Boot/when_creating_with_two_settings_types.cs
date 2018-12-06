using Machine.Specifications;

namespace Dolittle.Booting.for_Boot
{
    public class when_creating_with_two_settings_types
    {
        class first_settings_type : IRepresentSettingsForBootStage
        {
        }

        class second_settings_type : IRepresentSettingsForBootStage
        {
        }

        const string first = "First";
        const string second = "Second";
        const string third = "third";
        const string fourth = "fourth";

        static Boot result;

        static first_settings_type first_settings;
        static second_settings_type second_settings;


        Establish context = () =>
        {
            first_settings = new first_settings_type();
            second_settings = new second_settings_type();
        };

        Because of = () => result = new Boot(new IRepresentSettingsForBootStage[] { first_settings, second_settings });

        It should_hold_the_first_settings_type = () => result.GetSettingsByType(typeof(first_settings_type)).ShouldEqual(first_settings);
        It should_hold_the_second_settings_type = () => result.GetSettingsByType(typeof(second_settings_type)).ShouldEqual(second_settings);
    }
}