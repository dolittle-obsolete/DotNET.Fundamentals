/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Types;
using Dolittle.Resources.Configuration.Specs.given;
using Machine.Specifications;
using Moq;

namespace Dolittle.Resources.Configuration.Specs.for_TenantResourceManager.given
{
    public class an_instance_of_mongo_db_read_models_representation_and_a_system_providing_read_model_repository_configuration : all_dependencies
    {
        protected static Mock<IInstancesOf<IRepresentAResourceType>> instance_of_mongo_db_read_models_representation_mock;
        protected static Mock<ICanProvideResourceConfigurationsByTenant> a_system_provding_resource_configuration_for_read_models_mock;
        Establish context = () => 
        {
            var read_models_representation = new ResourceRepresentationForMongoDbReadModels();
            var read_models_representations = new List<IRepresentAResourceType>{read_models_representation};
            instance_of_mongo_db_read_models_representation_mock = new Mock<IInstancesOf<IRepresentAResourceType>>();
            instance_of_mongo_db_read_models_representation_mock.Setup(_ => _.GetEnumerator()).Returns(read_models_representations.GetEnumerator());

            a_system_provding_resource_configuration_for_read_models_mock = new Mock<ICanProvideResourceConfigurationsByTenant>();
            a_system_provding_resource_configuration_for_read_models_mock.Setup(_ => _.ConfigurationFor<ReadModelRepositoryConfiguration>(tenant_id, read_models_resource_type)).Returns(new ReadModelRepositoryConfiguration());
        };
    }
}