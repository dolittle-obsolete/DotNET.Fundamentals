// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Dolittle.Logging;

namespace Dolittle.DependencyInversion.Autofac
{
    /// <summary>
    /// An <see cref="Module"/> that binds the untyped <see cref="ILogger"/> to the correctly typed <see cref="ILogger{T}"/> when constructing components.
    /// </summary>
    public class LoggerModule : Module
    {
        /// <inheritdoc/>
        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing += OnComponentPreparing;
        }

        void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Append(
                new ResolvedParameter(
                    (p, _) => p.ParameterType == typeof(ILogger),
                    (p, c) => c.Resolve(typeof(ILogger<>).MakeGenericType(p.Member.DeclaringType))));
        }
    }
}