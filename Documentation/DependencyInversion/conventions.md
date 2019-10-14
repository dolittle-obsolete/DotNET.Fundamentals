---
title: Conventions
description: Overview of how conventions work
keywords: Conventions, Dependency Inversion
author: einari
weight: 4
aliases: 
    - /fundamentals/dotnet.fundamentals/dependencyinversion/conventions
---
Important part of how we think at Dolittle, is by enabling conventions.
Instead of having to configure everything explicitly, often at times there
are conventions in a project. You can use discovery mechanisms that
recognizes this and provides the bindings to the IoC container.

We provide a a default convention for hooking up any abstract class or interface
prefixed with `I` to an implementation with same name without the prefix.

Take for instance the following interface:

```csharp
public interface IFoo {}
```

...and then a concrete implementation of this:

```csharp
public class Foo : IFoo {}
```

These would be provided an automatic binding for by the default convention.

## Writing your own

It is very easy for you to write your own project specific convention to provide
bindings that is more complex than the default convention.
By implementing the interface `IBindingConvention` on a type, it will be
automatically discovered and hooked up.

```csharp
using Dolittle.DependencyInversion.Conventions;

public class DefaultConvention : IBindingConvention
{
    public bool CanResolve(Type service)
    {
        return service.Name.StartsWith("Something");
    }

    public void Resolve(Type service, IBindingBuilder builder)
    {
        builder.To(/* the type that implements the service asked for by CanResolve() */ )
    }
}
```
