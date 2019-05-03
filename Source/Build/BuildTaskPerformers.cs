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
    /// Represents an implementation of <see cref="IBuildTaskPerformers"/>
    /// </summary>
    [Singleton]
    public class BuildTaskPerformers : IBuildTaskPerformers
    {
        readonly IInstancesOf<ICanPerformBuildTask> _performers;
        readonly IBuildMessages _buildMessages;
        

        /// <summary>
        /// Initializes a new instance of <see cref="BuildTaskPerformers"/>
        /// </summary>
        /// <param name="performers"><see cref="IInstancesOf{ICanPerformPostBuildTasks}">Performers</see></param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for outputting build messages</param>
        public BuildTaskPerformers(
            IInstancesOf<ICanPerformBuildTask> performers,
            IBuildMessages buildMessages)
        {
            _buildMessages = buildMessages;
            _performers = performers;
        }

        /// <inheritdoc/>
        public void Perform()
        {
            _buildMessages.Information("Perform build tasks");
            _buildMessages.Indent();
            _performers.ForEach(_ =>
            {
                _buildMessages.Information($"{_.Message} (Task: '{_.GetType().AssemblyQualifiedName}')");
                _buildMessages.Indent();
                _.Perform();
                _buildMessages.Unindent();
            });

            _buildMessages.Unindent();
        }
    }
}