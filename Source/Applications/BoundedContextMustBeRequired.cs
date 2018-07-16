namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when thrown when an <see cref="Application"/> is created with an invalid <see cref="IApplicationStructure"/> because it's starting with a <see cref="BoundedContext"/> that's not required
    /// </summary>
    public class BoundedContextMustBeRequired : InvalidApplicationStructure
    {
        /// <summary>
        /// Initializes an instance of <see cref="BoundedContextMustBeRequired"/>
        /// </summary>
        /// <returns></returns>
        public BoundedContextMustBeRequired() : base()
        {
        }
    }
}