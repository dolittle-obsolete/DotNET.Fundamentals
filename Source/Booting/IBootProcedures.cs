/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Booting
{
    /// <summary>
    /// Defines the system that knows how to perform <see cref="ICanPerformBootProcedure">boot procedures</see>
    /// </summary>
    public interface IBootProcedures
    {
        /// <summary>
        /// Perform all <see cref="ICanPerformBootProcedure">boot procedures</see>
        /// </summary>
        void Perform();
    }
}