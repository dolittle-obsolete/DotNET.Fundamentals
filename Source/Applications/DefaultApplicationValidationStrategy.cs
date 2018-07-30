namespace Dolittle.Applications
{
    /// <summary>
    /// Represents the default <see cref="IApplicationValidationStrategy"/>
    /// </summary>
    public class DefaultApplicationValidationStrategy : IApplicationValidationStrategy
    {
        /// <inheritdoc/>
        public IApplicationStructureValidationStrategy ApplicationStructureValidationStrategy {get; }

        /// <summary>
        /// Initializes an instance of <see cref="DefaultApplicationValidationStrategy"/>
        /// </summary>
        public DefaultApplicationValidationStrategy()
        {
            ApplicationStructureValidationStrategy = new DefaultApplicationStructureValidationStrategy();
        }

        /// <inheritdoc/>
        public (bool isValid, InvalidApplication exception) Validate(IApplication application)
        {
            if (! HasAName(application)) 
            {
                var innerException = new MissingApplicationName();
                return (false, new InvalidApplication(innerException));
            }

            return (true, null);
        }

        bool HasAName(IApplication application)
        {
            return application.Name != null 
                && application.Name.Value != ApplicationName.NotSet 
                && ! string.IsNullOrEmpty(application.Name.Value);
        }
    }
}