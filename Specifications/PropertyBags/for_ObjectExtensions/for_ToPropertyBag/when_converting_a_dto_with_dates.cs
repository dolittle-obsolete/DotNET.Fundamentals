// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Time;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{
    [Subject(nameof(ObjectExtensions.ToPropertyBag))]
    public class when_converting_a_dto_with_dates
    {
        static DateTimeDto source;
        static dynamic result;

        Establish context = () => source = new DateTimeDto { DateTime = DateTime.UtcNow, DateTimeOffset = DateTimeOffset.UtcNow };

        Because of = () => result = source.ToPropertyBag();

        It should_create_a_property_bag = () => (result as PropertyBag).ShouldNotBeNull();

        It should_have_the_dates_properties = () =>
        {
            source.DateTimeOffset.ToUnixTimeMilliseconds().ShouldEqual((long)result.DateTimeOffset);
            source.DateTime.ToUnixTimeMilliseconds().ShouldEqual((long)result.DateTime);
        };
    }
}