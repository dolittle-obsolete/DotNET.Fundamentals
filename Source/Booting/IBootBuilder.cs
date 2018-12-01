/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq.Expressions;

namespace Dolittle.Booting
{
    /// <summary>
    /// Defines the builder for <see cref="Boot"/>
    /// </summary>
    public interface IBootBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTarget "></typeparam>
        void Set<TTarget>(Expression<Func<TTarget, object>> propertyExpression, object value) where TTarget:class, IRepresentSettingsForBootStage, new();

        /// <summary>
        /// Build the <see cref="Boot"/>
        /// </summary>
        /// <returns>Built <see cref="Boot"/></returns>
        Boot Build();
    }
}
