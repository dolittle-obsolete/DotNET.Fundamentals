namespace Dolittle.Applications
{

    /// <summary>
    /// Represents a null implementation of <see cref="IApplicationValidationStrategy"/>
    /// </summary>
    public class NullApplicationValidationStrategy : IApplicationValidationStrategy
    {
        /// <inheritdoc/>
        public IApplicationStructureValidationStrategy ApplicationStructureValidationStrategy {get; }   
        
        /// <summary>
        /// Initializes an instance of <see cref="NullApplicationStructureValidationStrategy"/>
        /// </summary>
        public NullApplicationValidationStrategy()
        {
            ApplicationStructureValidationStrategy = new NullApplicationStructureValidationStrategy();
        }

        /// <inheritdoc/>
        public (bool isValid, InvalidApplication exception) Validate(IApplication applicationStructure)
        {
            return (true, null);
        }
    }
}