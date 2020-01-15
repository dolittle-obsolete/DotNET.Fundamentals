// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias management;

using grpc = management::Dolittle.DependencyInversion.Management;

namespace Dolittle.DependencyInversion.Management
{
    /// <summary>
    /// Extensions for working with bindings.
    /// </summary>
    public static class BindingExtensions
    {
        /// <summary>
        /// Convert a native <see cref="Binding"/> to Grpc representation.
        /// </summary>
        /// /// <param name="binding"><see cref="Binding"/> to convert.</param>
        /// <returns>Converted <see cref="grpc.Binding"/>.</returns>
        public static grpc.Binding ToProtobuf(this Binding binding)
        {
            return new grpc.Binding
            {
                Service = binding.Service.AssemblyQualifiedName,
                Strategy = binding.Strategy.GetType().Name,
                StrategyData = binding.Strategy.GetTargetType()?.AssemblyQualifiedName ?? "N/A",
                Scope = binding.Scope.GetType().Name
            };
        }
    }
}