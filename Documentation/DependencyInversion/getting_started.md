---
title: Getting started
url: //fundamentals/dotnet-fundamentals/dependencyinversion/
description: Overview of how you can started
keywords: Getting started, Dependency Inversion
author: einari
weight: 1
aliases: 
    - /fundamentals/dotnet.fundamentals/dependencyinversion/getting_started
    - /fundamentals/dotnet-fundamentals/dependencyinversion/getting_started
---

## Overview
In our fundamentals, you'll find a package called [DependencyInversion](https://www.nuget.org/packages/Dolittle.DependencyInversion/).
[Dependency inversion](https://en.wikipedia.org/wiki/Dependency_inversion_principle) is the last principle
in the [SOLID principles](https://en.wikipedia.org/wiki/SOLID). We apply this principle as a step towards
decoupling, by focusing on the abstractions / interfaces to define our dependencies, we gain more flexibility
in the software to what concrete implementation we give. In addition to this principle, the package also represents
the concepts surrounding [Inversion of control](https://en.wikipedia.org/wiki/Inversion_of_control).
What this last concept gives us is a way to control creation and lifecycle from the outside. Combined,
we gain a lot of flexibility and enables us to make decisions at a higher level - we can also by
the virtue of interfaces being used, apply [cross-cutting concerns](https://en.wikipedia.org/wiki/Cross-cutting_concern).
This is a powerful tool that makes it easier to take decisions across types in a system.

With the dependencies defined as interfaces, we get added benefits when writing our specifications (automated tests).
We can now provide fake versions, or so called [mock objects](https://en.wikipedia.org/wiki/Mock_object) - making it
easier to test things in isolation, which is a core goal for [unit tests](https://en.wikipedia.org/wiki/Unit_testing) typically.

## Get started

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

* [Dolittle.Booting](https://www.nuget.org/packages/Dolittle.Booting/)
* [Dolittle.DependencyInversion.Autofac](https://www.nuget.org/packages/Dolittle.DependencyInversion.Autofac/)
* [Dolittle.DependencyInversion.Booting](https://www.nuget.org/packages/Dolittle.DependencyInversion.Booting/)
* [Dolittle.DependencyInversion.Conventions](https://www.nuget.org/packages/Dolittle.DependencyInversion.Conventions/)

In your `.csproj` this would be then:

```xml
<ItemGroup>
    <PackageReference Include="Dolittle.Booting" Version="4.*" />
    <PackageReference Include="Dolittle.DependencyInversion.Autofac" Version="4.*" />
    <PackageReference Include="Dolittle.DependencyInversion.Booting" Version="4.*" />
    <PackageReference Include="Dolittle.DependencyInversion.Conventions" Version="4.*" />
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

## Using from "bare metal"

If you're looking to just start using Dolittle and its dependency inversion, without
anything else, meaning that you're not booting it from our [SDK](/runtime/dotnet-sdk) or AspNet support.
First you'll need to add a package reference to the following:

* [Dolittle.DependencyInversion.Booting](https://www.nuget.org/packages/Dolittle.DependencyInversion.Booting/)
* [Dolittle.DependencyInversion.Conventions](https://www.nuget.org/packages/Dolittle.DependencyInversion.Conventions/)

In your `.csproj` file:

```xml
<ItemGroup>
    <PackageReference Include="Dolittle.DependencyInversion.Booting" Version="4.*" />
    <PackageReference Include="Dolittle.DependencyInversion.Conventions" Version="4.*" />
</ItemGroup>
```

You then need to have an actual implementation of a container, Dolittle supports
[Autofac](https://autofac.org) out of the box.

Add a package reference to the Autofac package:[Dolittle.DependencyInversion.Autofac](https://www.nuget.org/packages/Dolittle.DependencyInversion.Autofac/).

In your `.csproj` file:

```xml
<ItemGroup>
    <PackageReference Include="Dolittle.DependencyInversion.Autofac" Version="4.*" />
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
