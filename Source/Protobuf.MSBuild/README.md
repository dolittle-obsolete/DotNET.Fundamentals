# Protobuf MSBuild Suppport

This package is meant to make it more consistent for projects that generate code (proxies)
from `.proto` definitions.

By just adding a reference to this package, you'll get all the tooling set up correctly

The system is preconfigured, but you need to provide the path to where the main folder that
holds the `.proto` files. In addition, you can override the root for include files.
This is set to default `../`.

```xml
<PropertyGroup>
    <DolittleProtoProject>../[your folder]</DolittleProtoProject>
    <DolittleProtoRoot>../</DolittleProtoRoot>
</PropertyGroup>
```

## Upgrading Grpc.Tools

If one wants to change the version of the `Grpc.Tools` reference in the `.csproj`. It is vital
to also update the `Dolittle.Protobuf.MSBuild.targets` file to use the same version.