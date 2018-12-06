/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Booting.Stages;

namespace Dolittle.Booting
{
    /// <summary>
    /// Extensions for building <see cref="DiscoverySettings"/> 
    /// </summary>
    public static class BootProceduresBootBuilderExtensions
    {
        /// <summary>
        /// Skip any <see cref="ICanPerformBootProcedure">boot procedures</see>
        /// </summary>
        /// <param name="bootBuilder"><see cref="BootBuilder"/> to build</param>
        /// <returns>Chained <see cref="BootBuilder"/></returns>
        public static IBootBuilder SkipBootprocedures(this IBootBuilder bootBuilder)
        {
            bootBuilder.Set<BootProceduresSettings>(_ => _.Enabled, false);
            return bootBuilder;
        }
    }
}