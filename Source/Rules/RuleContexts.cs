/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Rules
{
    /// <summary>
    /// Represents an implementation of <see cref="IRuleContexts"/>
    /// </summary>
    public class RuleContexts : IRuleContexts
    {
        /// <inheritdoc/>
        public IRuleContext GetFor(object instance)
        {
            var ruleContext = new RuleContext(instance);
            return ruleContext;
        }
    }
}
