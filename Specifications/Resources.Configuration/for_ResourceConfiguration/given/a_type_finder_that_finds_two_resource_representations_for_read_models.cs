/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Dolittle.Resources.Configuration.Specs.given;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Resources.Configuration.Specs.for_ResourceConfiguration.given
{
    public class a_type_finder_that_finds_two_resource_representations_for_read_models : all_dependencies
    {
        protected static Mock<ITypeFinder> type_finder_mock;
        
        Establish context = () =>
        {
            var resource_type_representation_types = new List<Type>
            {
                typeof(ResourceRepresentationForMongoDbReadModels),
                typeof(ResourceRepresentationForMongoDbAnotherResourceType)
            };
            type_finder_mock = new Mock<ITypeFinder>();
            type_finder_mock.Setup(_ => _.FindMultiple<IRepresentAResourceType>()).Returns(resource_type_representation_types);
        };
    }
}