/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Resilience
{

    /// <summary>
    /// Defines a 
    /// </summary>
    public interface IPolicyFor<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        void Execute(Action action);
    }

    /// <summary>
    /// 
    /// </summary>
    public class PolicyName : ConceptAs<string>
    {

    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPoliciesFor<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IPolicyFor<T> GetFor(PolicyName name);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPolicyBuilderFor<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IPolicyFor<T> Build();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDefaultPolicy
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDefaultPolicyBuilder
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public interface ICanDefineDefaultPolicies
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        void Define(IDefaultPolicyBuilder builder);
    }

    /// <summary>
    /// Represents a system that is capable of defining resilience 
    /// </summary>
    public interface ICanDefinePoliciesFor<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="policyBuilder"></param>
        void Define(IPolicyBuilderFor<T> policyBuilder);
    }
}