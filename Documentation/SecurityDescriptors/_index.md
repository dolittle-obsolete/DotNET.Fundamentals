---
title: Security Descriptors
description: Overview of the fluent interface to secure commands and queries
keywords: Overview, Security, Command, Query
author: tomas
---
Securing your commands and queries is important. As a part of the Fundamentals
package there is a fluent interface that lets you describe restrictions to use of
commands and queries. You do this by implementing a SecurityDescriptor for your 
secureables.

There are extension methods and implementations for common patterns of restriction
in the SDK for Commands and Queries. To use these you would pull in the 
`Dolittle.Runtime.Commands.Security` and `Dolittle.Queries.Security` -namespaces. 
You can also create your own SecurityDescriptor without any of these conveniences.

Below are examples of security descriptors that secures commands in a namespace
by restricting them to only running if the current user is in the "admin" role, 
and queries for read-models in a different namespace to users that are authenticated.

These examples make use of the convenient extension methods in the namespaces 
mentioned previously available in the `Dolittle.Runtime` -package. If you do not
have an implementation of `ICanResolvePrincipal` a simple one is given at the 
end of this page.


```csharp
using Dolittle.Runtime.Commands.Security;
using Dolittle.Security;

namespace Domain.AccessControl
{
    public class RestrictAccessControlCommandsToAdminRole
    : Dolittle.Security.SecurityDescriptor
    {
        // needed to find the current user
        readonly ICanResolvePrincipal _principalResolver;

        public RestrictAccessControlCommandsToAdminRole(
            ICanResolvePrincipal resolver
        )
        {
            _principalResolver = resolver;

            When
                .Handling()
                .Commands()
                .InNamespace(
                    typeof(RestrictAccessControlCommandsToAdminRole).Namespace,
                    restriction =>
                        restriction
                            .UserFrom(_principalResolver)
                            .MustBeInRole("admin")
                );
        }
    }
}
```

An example of a security descriptor that restricts your queries for read models
to users with a claim of "name" is given below (i.e. they are authenticated with a name).

```csharp
using Dolittle.Queries.Security;
using Dolittle.Security;

namespace Read.Customers
{
    public class RestrictCustomerReadModelsToAuthenticatedUsers
    : Dolittle.Security.SecurityDescriptor
    {
        readonly ICanResolvePrincipal _principalResolver;

        public RestrictCustomerReadModelsToAuthenticatedUsers(
            ICanResolvePrincipal resolver
        )
        {
            _principalResolver = resolver;

            When
                .Fetching()
                .ReadModels()
                .InNamespace(
                    typeof(RestrictCustomerReadModelsToAuthenticatedUsers).Namespace,
                    restriction =>
                        restriction
                            .UserFrom(_principalResolver)
                            .HasClaimType("name")
                );
        }
    }
}
```

The Dolittle dependency-injection conventions will automatically discover any such
security descriptors and run them early in the pipeline (before input and business
validation).

This is an example of a very simple `ICanResolvePrincipal` set up in a binding provider
(see [DependencyInversion] for more information on how to set up bindings).

```csharp
using System.Security.Claims;
using System.Threading;
using Dolittle.DependencyInversion;
using Dolittle.Security;

namespace Core
{
    public class BasicBindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder
                .Bind<ICanResolvePrincipal>()
                .To(new SimplePrincipalProvider());
        }
    }

    internal class SimplePrincipalProvider : ICanResolvePrincipal
    {
        internal static AsyncLocal<ClaimsPrincipal> _currentClaimsPrincipal 
            = new AsyncLocal<ClaimsPrincipal>();

        public ClaimsPrincipal Resolve()
        {
            if(_currentClaimsPrincipal == null)
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }
            return _currentClaimsPrincipal.Value;
        }
    }
}

```

