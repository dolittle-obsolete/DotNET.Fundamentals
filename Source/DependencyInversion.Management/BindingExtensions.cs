/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.DependencyInversion.Management
{
    /// <summary>
    /// Extensions for working with bindings
    /// </summary>
    public static class BindingExtensions
    {
        /// <summary>
        /// Convert a native <see cref="DependencyInversion.Binding"/> to Grpc representation
        /// </summary>
        /// <param name="binding"><see cref="DependencyInversion.Binding"/> to convert</param>
        /// <returns>Converted <see cref="Binding"/></returns>
        public static Binding Convert(this DependencyInversion.Binding binding)
        {
            return new Binding {
                Service = binding.Service.AssemblyQualifiedName,
                Strategy = binding.Strategy.GetType().Name,
                StrategyData = binding.Strategy.GetTargetType()?.AssemblyQualifiedName ?? "N/A",
                Scope = binding.Scope.GetType().Name
            };
        }
    }
}