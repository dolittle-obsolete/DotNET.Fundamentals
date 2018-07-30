using Dolittle.PropertyBags;
using Machine.Specifications;
namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.given
{
    using Moq;
    using Dolittle.PropertyBags;
    using Dolittle.Types;
    using Dolittle.PropertyBags.Specs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class an_object_factory
    {
        protected static IObjectFactory instance;
        protected static IObjectFactory instance_with_two_factories_for_the_same_type;
        protected static Mock<IInstancesOf<ITypeFactory>> factory_instances;
        protected static Mock<IInstancesOf<ITypeFactory>> factory_instances_with_duplicates;

        Establish context = () => 
        {
            var factories = get_system_type_factories();

            var correct_factories = factories.ToList();
            correct_factories.Add(get_mock_user_defined_factory().Object);
            factory_instances = get_mock_instances_of_type_factory(correct_factories.ToArray());
            instance = new ObjectFactory(factory_instances.Object);

            var duplicate_factories = factories.ToList();
            duplicate_factories.Add(get_mock_user_defined_factory().Object);
            duplicate_factories.Add(get_mock_user_defined_factory().Object);
            factory_instances_with_duplicates = get_mock_instances_of_type_factory(duplicate_factories.ToArray());
            instance_with_two_factories_for_the_same_type = new ObjectFactory(factory_instances_with_duplicates.Object);
        };

        static Mock<IUserDefinedTypeFactory<RequiresSpecificConstructionByUser>> get_mock_user_defined_factory()
        {
            var user_defined = new Mock<IUserDefinedTypeFactory<RequiresSpecificConstructionByUser>>();
            user_defined.Setup(f => f.CanBuild(Moq.It.IsAny<Type>())).Returns((Type t) => t == typeof(RequiresSpecificConstructionByUser));
            user_defined.Setup(f => f.Build(Moq.It.IsAny<Type>(),Moq.It.IsAny<IObjectFactory>(),Moq.It.IsAny<PropertyBag>()))
                                .Returns((Type t, IObjectFactory f, PropertyBag p) => t == typeof(RequiresSpecificConstructionByUser) ? new RequiresSpecificConstructionByUser() : (RequiresSpecificConstructionByUser)null);

            return user_defined;
        }

        static Mock<IInstancesOf<ITypeFactory>> get_mock_instances_of_type_factory(params ITypeFactory[] factories)
        {
            var list = new List<ITypeFactory>(factories);
            var factory_instances = new Mock<IInstancesOf<ITypeFactory>>();
            factory_instances.Setup(f => f.GetEnumerator()).Returns(list.GetEnumerator());
            return factory_instances;
        }

        static IEnumerable<ITypeFactory> get_system_type_factories()
        {
            yield return new ImmutableTypeConstructorBasedFactory(new ConstructorProvider());
            yield return new MutableTypeSetterBasedFactory();
            yield return new MutableTypeConstructorBasedFactory();
        }
    }
}