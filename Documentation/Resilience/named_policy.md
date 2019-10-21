---
title: Named Policy
description: Overview of how to work with a named policy
keywords: Overview, Resilience
author: einari
weight: 2
aliases: 
    - /fundamentals/dotnet.fundamentals/resilience/named_policy
---
Named policies can be very useful is you want to have a policy that is used
by multiple different parts of your system and accessible by a well-known
name.

{{% notice information %}}
You don't have to define a named policy, if someone tries to access an undefined
policy - the [default policy]({{< relref default_policy >}}) will be used.
{{% /notice %}}

## Defining a named policy

To define a well known policy, all you need to do is add a C# class to your
project that implements the `IDefineNamedPolicy` interface.
From this, you leverage the API of [The Polly Project](https://github.com/App-vNext/Polly/wiki)
and return the actual policy or sets of policies that will be used.

Below is an example of something that will add a default policy:

```csharp
using Dolittle.Resilience;

public class NamedPolicyDefiner : IDefineNamedPolicy
{
    public string Name => "MyWellKnownPolicy";

    public Polly.Policy Define()
    {
        return Polly.Policy.NoOps();
    }
}
```

{{% notice warning %}}
There can only be one definition of a named policy per name. An exception will be
thrown if you try to add multiple definitions.
{{% /notice %}}

## Usage

To make use of a named policy, all you need to do is take a dependency to
`IPolicies` in your constructor and get the named policy. This will then automatically resolve
to the correct policy. Then, the code that needs resilience around it simply calls
the policy's correct `Execute` method.

Below is an example of how to use it:

```csharp
using Dolittle.Resilience;

public class MyResource
{
    IPolicy _policy;

    public MyResource(IPolicies policies)
    {
        _policy = policies.GetNamed("MyWellKnownPolicy");
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
