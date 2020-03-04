---
title: Container
description: Overview of how you can work with the container 
keywords: Overview, Dependency Inversion
author: einari
weight: 2
aliases: 
    - /fundamentals/dotnet.fundamentals/dependencyinversion/container
    - /fundamentals/dotnet-fundamentals/dependencyinversion/container
---
At the core of the `DependencyInversion` system sits `IContainer`.
This is something we build at startup and is considered immutable.
That means that it is expected that it will have all the bindings
readily configured upon creation. For the most part this means
by [conventions]({{< relref conventions >}}), but every now and then you need to
[provide specific bindings]({{< relref providing_bindings >}}.

Dolittle does not provide an implementation of an [IoC container](https://en.wikipedia.org/wiki/Inversion_of_control).
But we have created a very slim abstraction to represent what it means to
us in terms of what we see as most common scenarios.

That means that you have to pull in a specific IoC container and an implementation
for the Dolittle abstraction for it to automatically be used.

## Working with the container as a Service Locator

Normally your application really should only require just one or at least a very bare
minimum of calls at startup to the container to get things started. It should all
flow as dependencies from typically one starting point.

On rare occasions though, you might find yourself needing the `IContainer`
to be able to get instances your self - like a factory. This is referred to as the
[service locator pattern](https://en.wikipedia.org/wiki/Service_locator_pattern).

### Take as dependency

Anything can take a dependency to the `IContainer` and get the actual instance. This
will be a singleton for the application and therefore have only one instance.

Changing the `Foo` implementation slightly, we can get an instance of `IBar`as
follows:

```csharp
public class Foo : IFoo
{
    public Foo(IContainer container)
    {
        var bar = container.Get<IBar>();
    }
}
```

It is still being governed by the container itself, and all rules it decides on still apply.

### Get Container

There is another way to get the container, typically used if you have callback bindings,
but can be used in any setting. There are two different types of containers at play; one for
booting and used while building everything up and one for after booting. If your code runs
as part of the boot sequence - for instance in something that [provides bindings]({{< relref providing_bindings >}}),
you can take a dependency to something called `GetContainer`. This is a delegate you can call
whenever it suits you and you'll get the current container instance:

```csharp
using Dolittle.DependencyInversion;

public class BindingProvider : ICanProvideBindings
{
    readonly GetContainer _getContainer;

    public BindingProvider(GetContainer getContainer)
    {
        _getContainer = getContainer;
    }

    public void Provide(IBindingProviderBuilder builder)
    {
        builder.Bind<IBar>().To(() => _getContainer.Get<IBar>());
    }
}
```

This will then lazily call the `Func<T>` provided to ask you for an instance, and
the request can be delegated to the container after it has been retrieved.

## Adding support for a new container type

As Dolittle only have a limited offering for IoC containers and your favorite container
might not be supported. You can easily create support for it by creating a new
.NET Core class library targetting `netstandard2`.

Then you'll need to implement the `IContainer` interface and also the `ICanProvideContainer`
interface.

```csharp
using Dolittle.DependencyInversion;

public class ContainerProvider: ICanProvideContainer
{
    public IContainer Provide(IAssemblies assemblies, IBindingCollection bindings)
    {
        // Translate bindings into native bindings for the container being targeted

        var container = new MyContainer(/* whatever arguments it is expecting */);
        return container;
    }
}
```

{{% notice information %}}
We are looking at providing a set of automated specifications / tests that can be used to
be sure you're implementing all what is needed. This is especially around lifecycle, which
comes very important for things like singleton per tenant and others.
{{% /notice %}}
