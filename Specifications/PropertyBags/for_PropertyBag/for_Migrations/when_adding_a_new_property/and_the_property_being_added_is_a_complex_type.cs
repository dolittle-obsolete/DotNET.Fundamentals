/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Collections;
using Dolittle.PropertyBags.Migrations;
using Machine.Specifications;

namespace Dolittle.PropertyBags.for_PropertyBag.for_Migrations.when_adding_a_new_property
{
    [Subject(typeof(AddNewProperty<>),"Perform")]   
    public class and_the_property_being_added_is_a_complex_type
    {
        static ComplexType complex_type;
        static AddNewProperty<ComplexType> add_new_property;
        static NullFreeDictionary<string,object> target;

        Establish context = () => 
        {
            complex_type = new ComplexType("Naughty Zoot",11);
            add_new_property = new AddNewProperty<ComplexType>("AddedComplexType",complex_type);
            target = new NullFreeDictionary<string, object>();
        };

        Because of = () => add_new_property.Perform(target);

        It should_add_the_property = () => target.ContainsKey("AddedComplexType").ShouldBeTrue();
        It should_add_the_correct_value = () =>
        {
            dynamic added = target["AddedComplexType"];
            (added.MyFirstProperty as string).ShouldEqual("Naughty Zoot");
            ((int)added.MySecondProperty).ShouldEqual(11);
        };
    }       
}