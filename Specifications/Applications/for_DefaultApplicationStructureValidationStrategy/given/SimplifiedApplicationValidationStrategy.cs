namespace Dolittle.Applications.Specs.for_DefaultApplicationStructureValidationStrategy.given
{
    public class SimplifiedApplicationValidationStrategy : IApplicationValidationStrategy
    {
        public IApplicationStructureValidationStrategy ApplicationStructureValidationStrategy {get; }

        public SimplifiedApplicationValidationStrategy()
        {
            ApplicationStructureValidationStrategy = new DefaultApplicationStructureValidationStrategy();
        }
        public (bool isValid, InvalidApplication exception) Validate(IApplication applicationStructure)
        {
            return (true, null);
        }
    }
}