/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace doLittle.Security
{
    /// <summary>
    /// Represents a base implementation of<see cref="ISecurable"/>
    /// </summary>
    public class Securable : ISecurable
    {
        readonly List<ISecurityActor> _actors = new List<ISecurityActor>();

        /// <summary>
        /// Instantiates an instance of <see cref="Securable"/>
        /// </summary>
        /// <param name="securableDescription">Description of the Securable</param>
        public Securable(string securableDescription)
        {
            Description = securableDescription ?? string.Empty;
        }


        /// <inheritdoc/>
        public void AddActor(ISecurityActor actor)
        {
            _actors.Add(actor);
        }

        /// <inheritdoc/>
        public IEnumerable<ISecurityActor> Actors => _actors;


        /// <inheritdoc/>
        public virtual bool CanAuthorize(object actionToAuthorize)
        {
            return false;
        }

        /// <inheritdoc/>
        public virtual AuthorizeSecurableResult Authorize(object actionToAuthorize)
        {
            var result = new AuthorizeSecurableResult(this);
            foreach (var actor in _actors)
            {
                result.ProcessAuthorizeActorResult(actor.IsAuthorized(actionToAuthorize));
            }
            return result;
        }

        /// <inheritdoc/>
        public string Description { get; }
    }
}
