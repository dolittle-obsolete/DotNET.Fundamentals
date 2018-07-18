namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when a <see cref="IApplicationStructure"/> is invalid because it the Root <see cref="IApplicationStructureFragment"/> of the <see cref="IApplicationStructure"/>
    /// </summary>
    public class ApplicationStructureMustStartWithABoundedContext : InvalidApplicationStructure
    {
        
    }
}