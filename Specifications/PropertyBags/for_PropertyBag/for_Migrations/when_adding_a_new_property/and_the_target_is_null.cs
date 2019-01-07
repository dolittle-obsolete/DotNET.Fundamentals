using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_adding_a_new_property
{
    [Subject(typeof(AddNewProperty<>),"Perform")]   
    public class and_the_target_is_null
    {
        static AddNewProperty<int> add_new_int_property;
        static Exception exception;

        Establish context = () => add_new_int_property = new AddNewProperty<int>("NewIntProperty",100);

        Because of = () => exception = Catch.Exception(() => add_new_int_property.Perform(null));

        It should_fail_with_an_invalid_migration_source_exception = () => exception.ShouldBeOfExactType<InvalidMigrationSource>();
    }
}