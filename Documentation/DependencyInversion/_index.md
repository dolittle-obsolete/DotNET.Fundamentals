---
title: DependencyInversion
description: Overview of Dependency Inversion
keywords: Overview, Dependency Inversion
author: einari
aliases: 
    - /fundamentals/dotnet.fundamentals/dependencyinversion
---
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