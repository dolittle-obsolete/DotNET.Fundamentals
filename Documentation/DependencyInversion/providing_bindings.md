---
title: Providing bindings
description: Overview of how you can provide bindings
keywords: Overview, Dependency Inversion
author: einari
weight: 3
aliases: 
    - /fundamentals/dotnet.fundamentals/dependencyinversion/providing_bindings/
---
When a [convention]({{< relref conventions >}}) does not cover your
scenario, or you need to bind something based on configuration or similar.
You can then provide bindings by implementing binding provider.
By just implementing one and putting it in your project, it will be
discovered and automatically used.

The binding provider itself can take some dependencies, but only
those that are hooked up during booting - which is very limited to
internals of the fundamentals. However, you can take a dependency
to things like the `GetContainer` as described in the [container]({{< relref container >}})
article.

```csharp
using Dolittle.DependencyInversion;

public class BindingProvider : ICanProvideBindings
{
    public void Provide(IBindingProviderBuilder builder)
    {
        // Type binding
        builder.Bind<IBar>().To<Bar>();

        // Instance binding
        builder.Bind<IBar>().To(new Bar());

        // Callback binding
        builder.Bind<IBar>().To(() => new Bar());

        // Type callback binding
        builder.Bind<IBar>().To(() => typeof(Bar));

        // Open generic callback binding with binding context
        builder.Bind<IBarOf<>>().To((bindingContext) => new BarOf(bindingContext.Service));

        // Type callback binding
        builder.Bind<IBar>().To(() => typeof(Bar));

        // Open generic callback binding with binding context
        builder.Bind<IBarOf<>>().To((bindingContext) => typeof(BarOf<>).MakeGenericType(bindingContext.Service));
    }
}
```

You can also provide lifecycle to any bindings, everything is default transient.
Being transient means it will provide a new instance every time it is asked for.

{{% notice information %}}
If you have a type that has a longer lifecycle that what it takes dependency
onto and keeps the instance - it will live for the duration of the longest
lifecycle.
{{% /notice %}}

```csharp
using Dolittle.DependencyInversion;

public class BindingProvider : ICanProvideBindings
{
    public void Provide(IBindingProviderBuilder builder)
    {
        // Single instance per process
        builder.Bind<IBar>().To<Bar>().Singleton();

        // Single instance per tenant in a multi-tenanted system
        builder.Bind<IBar>().To<Bar>().SingletonPerTenant();
    }
}
```
