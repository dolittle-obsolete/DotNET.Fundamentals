using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Dolittle.PropertyBags.Migrations;
using Dolittle.Time;
using Machine.Specifications;

namespace Dolittle.PropertyBags.for_PropertyBag.for_Migrations.when_adding_a_new_property
{

    [Subject(typeof(AddNewProperty<DateTime>),"Perform")]   
    public class and_the_property_being_added_is_a_DateTime_of_non_UTC_kind
    {
        static AddNewProperty<DateTime> add_new_property;
        static NullFreeDictionary<string,object> target;

        static long date_time_utc_ticks;

        Establish context = () => 
        {
            var now = DateTime.Now;
            date_time_utc_ticks = now.ToUniversalTime().ToUnixTimeMilliseconds();
            add_new_property = new AddNewProperty<DateTime>("AddedProperty",now);
            target = new NullFreeDictionary<string, object>();
        };

        Because of = () => add_new_property.Perform(target);

        It should_add_the_property = () => target.ContainsKey("AddedProperty").ShouldBeTrue();
        It should_add_the_date_time_as_unix_time_milliseconds_utc = () => target["AddedProperty"].ShouldEqual(date_time_utc_ticks);
    }                       
}