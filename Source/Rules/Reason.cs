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
    public sealed class Reason
    {
        /// <summary>
        /// Gets the identifier for the <see cref="Reason"/>
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
        private Reason() { }

        /// <summary>
        /// Creates a new instance of <see cref="Reason"/> from a given unique identifier
        /// </summary>
        /// <param name="id">Unique identifier of the reason - this has to be a valid Guid in string format</param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns>A <see cref="Reason"/> instance</returns>
        /// <remarks>
        /// The format of the Guid has to be : 
        /// 00000000-0000-0000-0000-000000000000
        /// </remarks>
        public static Reason  Create(string id, string title, string description="")
        {
            return new Reason
            {
                Id = Guid.Parse(id),
                Title = title,
                Description = description
            };
        }

        /// <summary>
        /// Create a <see cref="Cause"/> without any arguments
        /// </summary>
        /// <returns>A new <see cref="Cause"/></returns>
        public Cause NoArgs()
        {
            return WithArgs(new{});
        }

        /// <summary>
        /// Create a <see cref="Cause"/> with given arguments
        /// </summary>
        /// <param name="args">Arguments to give</param>
        /// <returns><see cref="Cause"/></returns>
        /// <remarks>
        /// The arguments will be used in rendering of <see cref="Title"/> and <see cref="Description"/> strings
        /// </remarks>
        public Cause WithArgs(object args)
        {
            return new Cause(this, args);
        }
    }
}
