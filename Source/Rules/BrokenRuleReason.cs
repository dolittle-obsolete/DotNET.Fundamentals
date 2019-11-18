/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
namespace Dolittle.Rules
{
    /// <summary>
    /// Represents a reason for why a <see cref="IRule"/> is broken
    /// </summary>
    public sealed class BrokenRuleReason
    {
        /// <summary>
        /// Gets the identifier for the <see cref="BrokenRuleReason"/>
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the title of the <see cref="BrokenRule"/>
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the Description of the <see cref="BrokenRule"/>
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Private constructor - disables instantiation without going through <see cref="Create"/>
        /// </summary>
        private BrokenRuleReason() { }

        /// <summary>
        /// Creates a new instance of <see cref="BrokenRuleReason"/> from a given unique identifier
        /// </summary>
        /// <param name="id">Unique identifier of the reason - this has to be a valid Guid in string format</param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns>A <see cref="BrokenRuleReason"/> instance</returns>
        /// <remarks>
        /// The format of the Guid has to be : 
        /// 00000000-0000-0000-0000-000000000000
        /// </remarks>
        public static BrokenRuleReason  Create(string id, string title, string description="")
        {
            return new BrokenRuleReason
            {
                Id = Guid.Parse(id),
                Title = title,
                Description = description
            };
        }

        /// <summary>
        /// Create a <see cref="BrokenRuleReasonInstance"/> without any arguments
        /// </summary>
        /// <returns>A new <see cref="BrokenRuleReasonInstance"/></returns>
        public BrokenRuleReasonInstance NoArgs()
        {
            return WithArgs(new{});
        }

        /// <summary>
        /// Create a <see cref="BrokenRuleReasonInstance"/> with given arguments
        /// </summary>
        /// <param name="args">Arguments to give</param>
        /// <returns><see cref="BrokenRuleReasonInstance"/></returns>
        /// <remarks>
        /// The arguments will be used in rendering of <see cref="Title"/> and <see cref="Description"/> strings
        /// </remarks>
        public BrokenRuleReasonInstance WithArgs(object args)
        {
            return new BrokenRuleReasonInstance(this, args);
        }
    }
}
