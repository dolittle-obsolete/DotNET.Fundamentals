/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Represents a configuration element
    /// </summary>
    public abstract class ConfigurationValue<T> : ConceptAs<T>, IConfigurationValue
    {
        object IConfigurationValue.Value => base.Value;
    }
}