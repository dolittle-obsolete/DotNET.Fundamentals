# Services Sample

This sample shows the mechanics of building services and connecting a client to
exposed services in a host.

## Contracts

The contracts project is where you create the **proto** definition of services and messages.
This project has been configured to generate the necessary C# proxy objects.
Building this should yield C# files and can then be referenced used by both client and
server.

## Server

The server implements the services defined in the contract.
It uses the Dolittle wrappers to expose the service.
To run it, simply do a `dotnet run` from its folder.

## Client

The client project connects to the server.
It uses the Dolittle wrappers for making the client known.
To run it, simply do a `dotnet run` from its folder.