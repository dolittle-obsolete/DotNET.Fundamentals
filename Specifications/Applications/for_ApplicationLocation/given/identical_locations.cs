using System.Collections.Generic;
using doLittle.Applications;
using Machine.Specifications;

namespace Applications.for_ApplicationLocation.given
{
    public class identical_locations
    {
        protected static ApplicationLocation locationA;
        protected static ApplicationLocation locationB;

        Establish context = () => {
            locationA = new ApplicationLocation(GetSegments());
            locationB = new ApplicationLocation(GetSegments());
        };

        static IEnumerable<IApplicationLocationSegment> GetSegments()
        {
            var boundedContext = new BoundedContext("The Bounded Context");
            var module = new Module(boundedContext, "The Module");
            var feature = new Feature(module, "The Feature");

            return new IApplicationLocationSegment[] { 
                boundedContext,
                module, 
                feature
            };
        }
    }
}