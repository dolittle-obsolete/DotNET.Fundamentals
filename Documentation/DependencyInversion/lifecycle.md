---
title: Lifecyckle
description: Overview of how to work with lifecycle
keywords: Lifecycle, Dependency Inversion
author: einari
weight: 6
---
One of the traits of an IoC container is to govern the lifecycle outside of
the types needing the dependencies. This can typically be done at the
time of [providing bindings]({{< relref providing_bindings >}). But at times
the implementor knows what the lifecycle should be and it might be the right
responsibility for it to be decided at implementation. Thats where we have
a couple of attributes to govern this.

{{% notice information %}}
If you have a type that has a longer lifecycle that what it takes dependency
onto and keeps the instance - it will live for the duration of the longest
lifecycle.
{{% /notice %}}

## Adding a reference

Add a reference to the following package:

[Dolittle.Lifecycle](https://www.nuget.org/packages/Dolittle.Lifecycle/)

In your `.csproj` this would be then:

```xml
<ItemGroup>
    <PackageReference Include="Dolittle.Lifecycle" Version="3.*" />
</ItemGroup>
```

{{% notice information %}}
Adding packages to a project can also be done through using the dotnet CLI for adding package reference,
or using the capability in your IDE, such as in Visual Studio.
{{% /notice %}}

## Singleton

A singleton is an instance that there is only one of within a process. This
should be used with care, as you would be sharing this instance between tenants
and between operations done in your application. This is typically used for those
scenarios where performance matters due to heavy initialization.

```csharp
using Dolittle.Lifecycle;

namespace MyApp
{
    [Singleton]
    public class Foo : IFoo
    {

    }
}
```

By adding the attribute `[Singleton]` to the class, we now guarantee only one instance.

## Singleton per Tenant

In multi-tenanted applications, you might be looking at keeping in memory instances that
are per tenant. A tenant is defined through what is known as the `ExecutionContext` and
is configurable as to what strategy to use to distinguish which tenant is the current
tenant. As with the singleton lifecycle, you need to be fully aware of the implications
of doing so. Although having less risk associated with it with regards to tenancy leakage,
you might have requirements that could be broken if using this.

```csharp
using Dolittle.Lifecycle;

namespace MyApp
{
    [SingletonPerTenant]
    public class Foo : IFoo
    {

    }
}
```

By adding the attribute `[SingletonPerTenant]` to the class, we now guarantee only one instance
per tenant - depending on the `ExecutionContext`.