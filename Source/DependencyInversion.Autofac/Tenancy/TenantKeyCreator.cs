/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Text;
using Autofac;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Tenancy;

namespace Dolittle.DependencyInversion.Autofac.Tenancy
{
    /// <summary>
    /// Represents an implementation of <see cref="ITenantKeyCreator"/>
    /// </summary>
    public class TenantKeyCreator : ITenantKeyCreator
    {
        IExecutionContextManager _executionContextManager;

        /// <summary>
        /// Initializes a new instance of <see cref="TenantKeyCreator"/>
        /// </summary>
        /// <param name="containerBuilder"><see cref="ContainerBuilder"/> used for building the container</param>
        public TenantKeyCreator(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterBuildCallback(c => _executionContextManager = c.Resolve<IExecutionContextManager>());
        }


        /// <inheritdoc/>
        public string GetKeyFor(Binding binding, Type service)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(_executionContextManager.Current.Tenant);
            stringBuilder.Append("-");
            stringBuilder.Append(binding.Service.AssemblyQualifiedName);
            if( service.IsGenericType ) 
                service.GetGenericArguments().ForEach(_ => stringBuilder.Append($"-{_.AssemblyQualifiedName}"));

            return stringBuilder.ToString();
        }
   }
}