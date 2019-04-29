/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="IPostBuildTaskPerformers"/>
    /// </summary>
    public class PostBuildTaskPerformers : IPostBuildTaskPerformers
    {
        readonly IBuildMessages _buildMessages;
        readonly IInstancesOf<ICanPerformPostBuildTasks> _performers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="performers"></param>
        /// <param name="buildMessages"></param>
        public PostBuildTaskPerformers(IInstancesOf<ICanPerformPostBuildTasks> performers, IBuildMessages buildMessages)
        {
            _performers = performers;
            _buildMessages = buildMessages;
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