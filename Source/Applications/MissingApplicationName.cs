using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when a <see cref="IApplication"/> is missing its <see cref="ApplicationName"/>
    /// </summary>
    public class MissingApplicationName : InvalidApplication
    {
        /// <summary>
        /// Initializes an instance of <see cref="MissingApplicationName"/>
        /// </summary>
        /// <returns></returns>
        public MissingApplicationName() : base($"The {typeof(IApplication).FullName} is not built with a {typeof(ApplicationName).FullName}")
        {}
    }
}