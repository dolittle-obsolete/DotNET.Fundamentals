/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Booting
{
    /// <summary>
    /// Represents the necessary information to perform a boot
    /// </summary>
    public class Boot
    {
        Dictionary<Type, IRepresentSettingsForBootStage> _settings;

        /// <summary>
        /// Initialize a new instance of <see cref="Boot"/>
        /// </summary>
        /// <param name="settings"><see cref="IEnumerable{T}"/> of <see cref="IRepresentSettingsForBootStage"/></param>
        public Boot(IEnumerable<IRepresentSettingsForBootStage> settings)
        {
            _settings = settings.ToDictionary(_ => _.GetType(), _ => _);
        }

        /// <summary>
        /// Get settings with a specific type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IRepresentSettingsForBootStage GetSettingsByType(Type type)
        {
            if( !_settings.ContainsKey(type))
            {
                var settings = Activator.CreateInstance(type) as IRepresentSettingsForBootStage;
                _settings[type] = settings;
                return settings;
            }
            return _settings[type];
        }
    }
}
