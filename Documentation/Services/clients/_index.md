---
title: Service Clients
description: Overview of how clients of services work
keywords: Overview, Services, Clients
author: einari
---
With the gRPC libraries that Dolittle leverages, there is the concept of a
`Client`. When you typically work with a service, defining its **contract**
as a `.proto` definition of the service, the tooling provides you with a
code generated representation of the service. The `Dolittle.Services.Client`
package helps you work with these services in a natural way and fully supporting
the use of the configured IoC Container - letting you take a dependency to a client
and be sure that it will connect in a reliable way. In addition to this, you can
also trust it to be resilient; if the server that it is connected to goes down -
it will retry based on the retry policy.

## Sample

There is an end to end sample that can be found [here](https://github.com/dolittle-fundamentals/DotNET.Fundamentals/tree/master/Samples/Services).
This shows the **server**, the **client** and the **contract** project that acts
as the agreed upon contract between the two.

## Execution Context

In Dolittle you'll find something called the `ExecutionContext` in the namespace
`Dolittle.Execution`. The purpose of this is to maintain the current context in
which your currently running code is in. This consists of things like current tenant,
claims of the current user and more.

Part of working with services is also to maintain this `ExecutionContext` across
remote calls. The system is automatically hooked up to intercept every call and
put this context in a serialized form as part of the header. The contract and how
the payload looks like can be found [here](https://github.com/dolittle-fundamentals/Contracts/tree/master/Source/Execution).

The client will automatically add this in a header key called `executioncontext-bin`.
In addition on the server side, similarly; every call is intercepted and the
header is decoded and the current call path will have the correct `ExecutionContext`.

## Known Clients

The client binding system works around the concept of known clients. A known client
is a client we know which service it represents, what type the client implementation
is and what the visibility of the endpoint it is connecting to is.

In order for the system to know about these, you need to implement an interface
called `IKnowAboutClients` and return all the definitions you know about. The system
will at startup discover any implementations of these and configure everything accordingly.
This is the thing that enables the IoC bindings to just work.

Below is a sample

```csharp
public class SampleServiceClients : IKnowAboutClients
{
    public IEnumerable<Client> Clients => new[]
    {
        new Client(EndpointVisibility.Private, typeof(TestServiceClient), TestService.Descriptor)
    };
}
```
