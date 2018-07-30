namespace Dolittle.Applications
{
    /// <summary>
    /// Represents a null implementation of <see cref="IApplicationStructureValidationStrategy"/>
    /// </summary>
    public class NullApplicationStructureValidationStrategy : IApplicationStructureValidationStrategy
    {
        /// <inheritdoc/>
        public (bool isValid, InvalidApplicationStructure exception) Validate(IApplicationStructure applicationStructure)
        {
            return (true, null);
        }
    }
}