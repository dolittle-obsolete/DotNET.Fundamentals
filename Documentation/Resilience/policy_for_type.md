---
title: Policy for type
description: Overview of how to work with a policy for a type
keywords: Overview, Resilience
author: einari
weight: 3
---
Policies for specific types are accessible and defined linked to a type. These can
be very useful to use for specific resources needing resilience and makes it easy
and clear to scope it to that resource specifically.

{{% notice information %}}
You don't have to define a policy for a type, if someone tries to access an undefined
policy - the [default policy]({{< relref default_policy >}}) will be used.
{{% /notice %}}

## Defining a policy for a type

To define a well known policy, all you need to do is add a C# class to your
project that implements the `IDefinePolicyForType` interface.
From this, you leverage the API of [The Polly Project](https://github.com/App-vNext/Polly/wiki)
and return the actual policy or sets of policies that will be used.

Below is an example of something that will add a default policy:

```csharp
using Dolittle.Resilience;

public class NamedPolicyDefiner : IDefinePolicyForType
{
    public Type Name => typeof(MyResource);

    public Polly.Policy Define()
    {
        return Polly.Policy.NoOps();
    }
}
```

{{% notice warning %}}
There can only be one definition of a policy per type. An exception will be
thrown if you try to add multiple definitions.
{{% /notice %}}

## Usage

To make use of a policy for a type, all you need to do is take a dependency to
`IPolicyFor<MyResource>` in your constructor. This will then automatically resolve
to the correct policy. Then, the code that needs resilience around it simply calls
the policy's correct `Execute` method.

Below is an example of how to use it:

```csharp
using Dolittle.Resilience;

public class MyResource
{
    IPolicyFor<MyResource> _policy;

    public MyResource(IPolicyFor<MyResource> policy)
    {
        _policy = policy;
    }

    public void DoStuffToResource()
    {
        _policy.Execute(() => {
            /*
                Perform actions on the resource
            */
        });
    }
}
```
