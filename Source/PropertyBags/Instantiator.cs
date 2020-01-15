// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq.Expressions;
using System.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Helps create a delegate for instantiating an instance of a Type which requires
    /// parameters in the constructor.
    /// </summary>
    public static class Instantiator
    {
        /// <summary>
        /// Creates a delegate instance that can be used to instantiate an instance with the
        /// provided values.
        /// </summary>
        /// <param name="ctor">The <see cref="ConstructorInfo"/>.</param>
        /// <returns><see cref="InstanceActivator"/> instance.</returns>
        public static InstanceActivator GetInstanceActivator(ConstructorInfo ctor)
        {
            var paramsInfo = ctor.GetParameters();

            var param = Expression.Parameter(typeof(object[]), "args");

            var argsExp = new Expression[paramsInfo.Length];
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                var index = Expression.Constant(i);
                var paramType = paramsInfo[i].ParameterType;
                argsExp[i] = Expression.Convert(Expression.ArrayIndex(param, index), paramType);
            }

            var newExp = Expression.New(ctor, argsExp);
            var lambda = Expression.Lambda(typeof(InstanceActivator), newExp, param);
            return (InstanceActivator)lambda.Compile();
        }
    }
}