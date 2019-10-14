---
title: FactoryFor<>
description: Overview of how to work with the factory for
keywords: Factory, Creation, Dependency Inversion
author: einari
weight: 7
aliases: 
    - /fundamentals/dotnet.fundamentals/dependencyinversion/factory_for
---
Instead of working with the container as a service locator directly,
as described [here]({{< relref container >}}); you can represent what
you are trying to accomplish through a factory delegate.

`FactoryFor<>` can be taken as a dependency and you can then call upon
it to get an instance - it will then be a type safe instance and
a much more cleaner way rather than working directly with the
container.

```csharp
using Dolittle.DependencyInversion;

public class Foo : IFoo
{
    public Foo(FactoryFor<IBar> getBar)
    {
        var bar = getBar();
    }
}
```