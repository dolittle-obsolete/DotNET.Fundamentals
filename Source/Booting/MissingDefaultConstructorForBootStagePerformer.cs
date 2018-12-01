/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Dolittle.Booting
{
    /// <summary>
    /// The exception the gets thrown if a <see cref="ICanPerformPartOfBootStage"/> is missing a default constructor
    /// </summary>
    public class MissingDefaultConstructorForBootStagePerformer : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingDefaultConstructorForBootStagePerformer"/>
        /// </summary>
        /// <param name="type">Type of <see cref="ICanPerformPartOfBootStage"/> that is misssing the default constructor</param>
        public MissingDefaultConstructorForBootStagePerformer(Type type) : base($"Boot stage performer of type '{type.AssemblyQualifiedName}' is missing a default constructor") {}
    }
}
