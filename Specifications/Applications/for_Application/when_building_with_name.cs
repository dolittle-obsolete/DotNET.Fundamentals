/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_Application
{
    public class when_starting_building_with_name
    {
        const string application_name = "My Application";
        static IApplication result;

        Because of = () => result = Application.WithName(application_name).Build(new NullApplicationValidationStrategy());

        It should_hold_name = () => ((string) result.Name).ShouldEqual(application_name);
    }
}
