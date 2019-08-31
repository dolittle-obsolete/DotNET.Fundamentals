---
title: Exposing host type
description: How to expose a gRPC host type
keywords: Overview, gRPC
author: einari
weight: 1
---
A **host type** is the definition of an entrypoint that expose multiple services on it.
You can think of it as the thing that is exposing a TCP socket on a specific port.
Every **host type** can have multiple gRPC services that gets bound.
The purpose of this is to provide the ability to separate different concerns. For instance
if your system has the need to expose a way for clients to interact with your system and
at the same time you want to expose a management API. These two types of services are
not the same, they are not secured in the same way and covers different concerns.

## Host Type Representation

To represent a **host type**, all you need to do is implement an interface that does just that;
`IRepresentHostType`:

```csharp
using Dolittle.Grpc;

public class MyHostTypeRepresentation : IRepresentHostType
{
    public HostType Identifier => "My Host Type";

    public Type BindingInterface => typeof(ICanBindMyHostTypeServices);

    public HostConfiguration Configuration => new HostConfiguration(50100); // TCP Port
}
```

{{% notice note %}}
The instantiation of the discovered **host type** representation is done through the
IoC container, which means you can take dependencies to other things - such as configuration
that is sourced from a file or other places
{{% /notice %}}

The `BindingInterface` property points to a type that represents the definition of a
contract that can bind services for your host type. This is a type that will be used
in a discovery and you can have multiple of these. This provides you a good way to
keep your providers of services for different purposes within the same host type separate
and also provides you with an extensibility point.
