/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Assemblies
{
    /// <summary>
    /// 
    /// </summary>
    public class TriggerContext
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public interface IBehavior
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public delegate void Triggered();

    /// <summary>
    /// 
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// 
        /// </summary>
        event Triggered Triggered;
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IAction
    {

        /// <summary>
        /// 
        /// </summary>
        void Perform();

    }
}