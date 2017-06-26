/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Types.Specs.for_TypeFinder
{
    [Subject(typeof(TypeFinder))]
    public class when_constructing : given.a_type_finder
    {
        Because of = () => {}; // Construction done in the context

        It should_populate_map = () => contract_to_implementors_map_mock.Verify(c=>c.Feed(types), Times.Once);
    }
}