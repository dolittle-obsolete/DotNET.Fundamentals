/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.Types;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="IPostBuildTaskPerformers"/>
    /// </summary>
    [Singleton]
    public class PostBuildTaskPerformers : IPostBuildTaskPerformers
    {
        readonly IInstancesOf<ICanPerformPostBuildTasks> _performers;
        readonly IBuildMessages _buildMessages;
        

        /// <summary>
        /// Initializes a new instance of <see cref="PostBuildTaskPerformers"/>
        /// </summary>
        /// <param name="performers"><see cref="IInstancesOf{ICanPerformPostBuildTasks}">Performers</see></param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for outputting build messages</param>
        public PostBuildTaskPerformers(
            IInstancesOf<ICanPerformPostBuildTasks> performers,
            IBuildMessages buildMessages)
        {
            _buildMessages = buildMessages;
            _performers = performers;
        }

        /// <inheritdoc/>
        public void Perform()
        {
            _performers.ForEach(_ =>
            {
                _buildMessages.Information($"Performing post build task '{_.GetType().AssemblyQualifiedName}'");
                _.Perform();
            });
        }
    }
}