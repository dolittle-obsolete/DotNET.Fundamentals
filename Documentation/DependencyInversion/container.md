---
title: Container
description: Overview of how you can work with the container 
keywords: Overview, Dependency Inversion
author: einari
weight: 2
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

## Getting started

The simplest thing for getting started is to use the built-in booting system
which will take care of setting everything up in order.

Start by creating a folder for where you want to try this out, or skip this if you
want to retrofit into an existing application, then you create a new console project.

```shell
$ dotnet new console
```

Or if you prefer using other tooling, like your IDE (Visual Studio or similar) for doing this,
that's fine as well. As long as it is compatible with `netstandard2`.

Then add a reference to the following packages:

[Dolittle.Booting](https://www.nuget.org/packages/Dolittle.Booting/)
[Dolittle.DependencyInversion.Autofac](https://www.nuget.org/packages/Dolittle.DependencyInversion.Autofac/)
[Dolittle.DependencyInversion.Booting](https://www.nuget.org/packages/Dolittle.DependencyInversion.Booting/)

In your `.csproj` this would be then:

```xml
<ItemGroup>
    <PackageReference Include="Dolittle.Booting" Version="3.*" />
    <PackageReference Include="Dolittle.DependencyInversion.Autofac" Version="3.*" />
    <PackageReference Include="Dolittle.DependencyInversion.Booting" Version="3.*" />
</ItemGroup>
```

{{% notice information %}}
Adding packages to a project can also be done through using the dotnet CLI for adding package reference,
or using the capability in your IDE, such as in Visual Studio.
{{% /notice %}}

Lets then add a file with  `IBar` interface and a `Bar` implementation and then
`IFoo` interface and a `Foo` implementation that takes `IBar` as a dependency.

```csharp
namespace MyApp
{
    public interface IBar {}

    public class Bar {}

    public interface IFoo {}

    public class Foo : IFoo
    {
        public Foo(IBar bar)
        {
            // bar should hold an instance automatically bound to Bar
        }
    }
}
```

In your Program.cs - or where you want to get started with this, you'd do the following:

```csharp
using Dolittle.Booting;
using Microsoft.Extensions.Logging;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();

            var bootLoaderResult = BootLoader.Configure(_ =>
                .Development()
                .UseLoggerFactory(loggerFactory)
            ).Start();

            var foo = bootLoaderResult.Container.Get<IFoo>()
            // foo should automatically be bound to Foo
        }
    }
}
```

Running this, for instance with the debugger attached - you should now see instances
for both `foo` and `bar` and also recursively instantiated.

Since there is a default [convention]({{< relref conventions >}}) for hooking up any abstract class or interface
prefixed with `I` to an implementation with same name without the prefix, this will just work.

For more complex binding scenarios, you want to [provide bindings]({{< relref provide_bindings >}}) yourself.

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
as part of the boot sequence - for instance in something that [provides bindings]({{< relref provide_bindings >}}),
you can take a dependency to something called `GetContainer`. This is a delegate you can call it
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

## Using from scratch

If you're looking to just start using Dolittle and its dependency inversion, without
anything else, meaning that you're not booting it from our [SDK](/runtime/dotnet-sdk) or AspNet support.
First you'll need to add a package reference to the following:

[Dolittle.DependencyInversion.Booting](https://www.nuget.org/packages/Dolittle.DependencyInversion.Booting/)

In your `.csproj` file:

```xml
<ItemGroup>
    <PackageReference Include="Dolittle.DependencyInversion.Booting" Version="3.*" />
</ItemGroup>
```

You then need to have an actual implementation of a container, Dolittle supports
[Autofac](https://autofac.org) out of the box.

Add a package reference to the Autofac package:[Dolittle.DependencyInversion.Autofac](https://www.nuget.org/packages/Dolittle.DependencyInversion.Autofac/).

In your `.csproj` file:

```xml
<ItemGroup>
    <PackageReference Include="Dolittle.DependencyInversion.Autofac" Version="3.*" />
</ItemGroup>
```

{{% notice information %}}
Adding packages to a project can also be done through using the dotnet CLI for adding package reference,
or using the capability in your IDE, such as in Visual Studio.
{{% /notice %}}

### Booting

Throughout the fundamentals we have different boot stages, and used from a higher level - it will combine the
stages and run them in order. But using something like the dependency inversion part only on its own means
that we have to get it "booted". This basically just means to get it configured and started.

If you're using more of Dolittle, from typically the [SDK](/runtime/dotnet-sdk) or AspNet support,
you wouldn't need to deal with this at this level. It would all be taken care of for you.

Booting is done by calling the `.Start()` method on the `Boot` class in the `Booting`namespace of `DependencyInversion.
It does however require a couple of things for it to be able to do so - as we are doing some discovery at startup to get [conventions]({{< relref conventions >}}) and other things - like your bindings automatically hooked up.

```csharp
using Dolittle.Assemblies;
using Dolittle.Assemblies.Configuration;
using Dolittle.Assemblies.Rules;
using Dolittle.IO;
using Dolittle.Logging;
using Dolittle.Scheduling;
using Dolittle.Types;

void BootContainer()
{
    var assembliesConfigurationBuilder = new AssembliesConfigurationBuilder();
    assembliesConfigurationBuilder
        .ExcludeAll()
        .ExceptProjectLibraries()
        .ExceptDolittleLibraries();

    var entryAssembly = Assembly.GetEntryAssembly();

    var assembliesConfiguration = new AssembliesConfiguration(assembliesConfigurationBuilder.RuleBuilder);
    var assemblyFilters = new AssemblyFilters(assembliesConfiguration);

    var assemblyProvider = new AssemblyProvider(
        new ICanProvideAssemblies[] { defaultAssemblyProvider ?? new DefaultAssemblyProvider(logger, entryAssembly) },
        assemblyFilters,
        new AssemblyUtility(),
        logger
    );

    var assemblies = new Assemblies(entryAssembly, assemblyProvider);

    contractToImplementorsMap = new ContractToImplementorsMap(scheduler);
    contractToImplementorsMap.Feed(entryAssembly.GetTypes());
    var typeFeeder = new TypeFeeder(scheduler, logger);
    typeFeeder.Feed(assemblies, contractToImplementorsMap);

    var typeFinder = new TypeFinder(contractToImplementorsMap);
    var logAppenders = new LogAppenders(new ICanConfigureLogAppenders[0], new DefaultLogAppender());
    var logger = new Logger(logAppenders);

    var scheduler = new AsyncScheduler();
    var fileSystem = new FileSystem();

    var bootResult = Dolittle.DependencyInversion.Booting.Boot.Start(
        assemblies,
        typeFinder,
        scheduler,
        fileSystem,
        logger
    );
}
```

At the end in the `bootResult` you'll then have access to the `IContainer` instance and also have
all the bindings that was discovered by conventions or provided by [providers]({{< relref providing_bindings >}}).
