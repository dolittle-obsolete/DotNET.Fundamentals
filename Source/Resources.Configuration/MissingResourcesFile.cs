using System;
using System.Runtime.Serialization;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// The Exception that gets thrown when the resources file is not present
    /// </summary>
    public class MissingResourcesFile : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="MissingResourcesFile"/>
        /// </summary>
        public MissingResourcesFile()
            : base($"Could not find .dolittle/resources.json")
        {
        }
    }
}