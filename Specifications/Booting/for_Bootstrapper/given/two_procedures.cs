using System.Collections.Generic;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Booting.Specs.for_Bootstrapper.given
{
    public class two_procedures : all_dependencies
    {
        protected static Mock<ICanPerformBootProcedure> first_procedure;
        protected static bool first_procedure_can_perform = true;
        protected static Mock<ICanPerformBootProcedure> second_procedure;
        protected static bool second_procedure_can_perform = true;

        Establish context = () => 
        {
            first_procedure = new Mock<ICanPerformBootProcedure>();
            first_procedure.Setup(_ => _.CanPerform()).Returns(() => first_procedure_can_perform);
            second_procedure = new Mock<ICanPerformBootProcedure>();
            second_procedure.Setup(_ => _.CanPerform()).Returns(() => second_procedure_can_perform);

            var instances = new Mock<IInstancesOf<ICanPerformBootProcedure>>();
            var listOfInstances = new List<ICanPerformBootProcedure>();
            listOfInstances.AddRange(new[] {
                first_procedure.Object,
                second_procedure.Object
            });
            instances.Setup(_ => _.GetEnumerator()).Returns(listOfInstances.GetEnumerator());

            container.Setup(_ => _.Get<IInstancesOf<ICanPerformBootProcedure>>()).Returns(instances.Object);
        };
    }
}