---
title: Binding services
description: How to bind your gRPC for a service type
keywords: Overview, gRPC
author: einari
weight: 2
aliases: 
    - /fundamentals/dotnet.fundamentals/services/binding_services
    - /fundamentals/dotnet-fundamentals/services/binding_services
---
gRPC services are typically defined in a `.proto` file and using the gRPC tooling you can
generate a proxy representation for your programming language. These are then types that
can be provided and exposed through a service definition. This is called binding of services.

## Binding interface

In order for you to provide services for a **service type**, you'll need to have an interface
that represents this binder. This type must implement the `ICanBindServices` interface.

```csharp
using Dolittle.Services;

public interface ICanBindMyServiceTypeServices : ICanBindServices
{
}
```

## Creating services

A service is typically defined in a `.proto` file.

```protobuf
// The greeting service definition.
service Greeter {
  // Sends a greeting
  rpc SayHello (HelloRequest) returns (HelloReply) {}
}

// The request message containing the user's name.
message HelloRequest {
  string name = 1;
}

// The response message containing the greetings
message HelloReply {
  string message = 1;
}
```

From this, we want to generate the proper C# proxy objects. This is best done using the
[gRPC tools](https://github.com/grpc/grpc/blob/master/src/csharp/BUILD-INTEGRATION.md).

{{% notice note %}}
The [gRPC tools](https://www.nuget.org/packages/Grpc.Tools/) contains the core CLI tools
to generate, and if you want to have fine grained control over generation, you can have
a look at [this](https://github.com/grpc/grpc/blob/master/src/csharp/generate_proto_csharp.sh)
for reference.

Also, Microsoft has a writeup on their wrapper for ASP.NET Core on this.
Read [this](https://docs.microsoft.com/en-us/aspnet/core/grpc/basics?view=aspnetcore-3.0)
for more details.
{{% /notice %}}

From the proxies generated, you'll get a base class for your service that you will need
to implement. This is the actual service we will be using.

## Binding services

Once you have your services defined and proxy generated and also the **service type** binding interface,
you have the type that will be used to bind the actual services specifically for your **service type**.
All you and anyone building on top need to do then is to implement a type that implements this interface.

```csharp
using System.Collections.Generic;
using Grpc.Core;
using Dolittle.Services;

public class MyServiceTypeServices : ICanBindMyServiceTypeServices
{
    public IEnumerable<ServerServiceDefinition> BindServices()
    {
        var service = new GreeterService();
        return new ServiceServiceDefinition[] {
            Greeter.BindService(service)
        };
    }
}
```

Your service will now be exposed and reliably hosted on the port coming from the configuration for
the **service type**.
