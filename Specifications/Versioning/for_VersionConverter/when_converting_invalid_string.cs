/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace Dolittle.Versioning.for_VersionConverter
{
    public class when_converting_invalid_string : given.a_version_converter
    {
        static Exception result;
        
        Because of = () => result = Catch.Exception(() => version_converter.FromString("Blah"));

        It should_throw_invalid_version_string = () => result.ShouldBeOfExactType(typeof(InvalidVersionString));
    }
}