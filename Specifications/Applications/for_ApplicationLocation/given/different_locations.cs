using System.Collections.Generic;
using doLittle.Applications;
using Machine.Specifications;

namespace Applications.for_ApplicationLocation.given
{
    public class different_locations
    {
        protected static ApplicationLocation locationA;
        protected static ApplicationLocation locationB;

        Establish context = () => {
            locationA = new ApplicationLocation(GetSegments("BoundedContext1","Module1","Feature1"));
            locationB = new ApplicationLocation(GetSegments("BoundedContext2","Module2","Feature2"));
        };

        static IEnumerable<IApplicationLocationSegment> GetSegments(string boundedContextName, string moduleName, string featureName)
        {
            var boundedContext = new BoundedContext(boundedContextName);
            var module = new Module(boundedContext, moduleName);
            var feature = new Feature(module, featureName);

            return new IApplicationLocationSegment[] { 
                boundedContext,
                module, 
                feature
            };
        }
    }
}