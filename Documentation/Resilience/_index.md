---
title: Resilience
description: Overview of Resilience
keywords: Overview, Resilience
author: einari
---
In our fundamentals, you'll find a package called [Resilience](https://www.nuget.org/packages/Dolittle.Resilience/).
The purpose of this package is to make it possible to define policies that are to be used for
handling resilience and transient faults.

It is built on top of [The Polly Project](http://www.thepollyproject.org) with a lightweight
wrapper that enables a couple of scenarios:

- Typed policies
- Named policies
- Automatic fallback to a default policy
- IoC friendly
- Testable
