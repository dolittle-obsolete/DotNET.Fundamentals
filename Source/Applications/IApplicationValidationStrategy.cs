namespace Dolittle.Applications
{
    /// <summary>
    /// Represents a strategy for validating a <see cref="IApplication"/>
    /// </summary>
    public interface IApplicationValidationStrategy
    {
         
        /// <summary>
        /// The validation strategy for the <see cref="IApplicationStructure"/>
        /// </summary>
        /// <returns></returns>
         IApplicationStructureValidationStrategy ApplicationStructureValidationStrategy {get; }

        /// <summary>
        /// Checks the validity of the <see cref="IApplication"/> and returns a tuple containing the appropriate <see cref="InvalidApplication"/> exception if the <see cref="IApplication"/> isn't valid.
        /// </summary>
        /// <param name="applicationStructure"></param>
        /// <returns></returns>
         (bool isValid, InvalidApplication exception) Validate(IApplication applicationStructure);

    }
}