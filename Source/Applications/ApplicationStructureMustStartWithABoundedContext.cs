namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when an <see cref="Application"/> is created with an invalid <see cref="IApplicationStructure"/> because it's not starting with a <see cref="BoundedContext"/>
    /// </summary>
    public class ApplicationStructureMustStartWithABoundedContext : InvalidApplicationStructure
    {
        
    }
}