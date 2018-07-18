namespace Dolittle.Applications
{
    /// <summary>
    /// Represents a strategy for validating a <see cref="IApplicationStructure"/>
    /// </summary>
    public interface IApplicationStructureValidationStrategy
    {
        /// <summary>
        /// Checks the validity of the <see cref="IApplicationStructure"/> and returns a tuple containing the appropriate <see cref="InvalidApplicationStructure"/> exception if the <see cref="IApplicationStructure"/> isn't valid.
        /// </summary>
        /// <param name="applicationStructure"></param>
        /// <returns></returns>
         (bool isValid, InvalidApplicationStructure exception) Validate(IApplicationStructure applicationStructure);
    }
}