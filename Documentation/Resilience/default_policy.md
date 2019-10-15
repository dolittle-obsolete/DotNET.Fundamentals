---
title: Default Policy
description: Overview of how to work with a default policy
keywords: Overview, Resilience
author: einari
weight: 1
aliases: 
    - /fundamentals/dotnet.fundamentals/resilience/default_policy
---
The resilience system has the concept of a default policy.
This is the policy that gets used if there is no specific policy for a
type or a named policy.

{{% notice information %}}
You don't have to define a default policy, if no default policy has been
defined, a pass-through policy will be used.
{{% /notice %}}

## Defining the default policy

To define the default policy, all you need to do is add a C# class to your
project that implements the `IDefineDefaultPolicy` interface.
From this, you leverage the API of [The Polly Project](https://github.com/App-vNext/Polly/wiki)
and return the actual policy or sets of policies that will be used.

Below is an example of something that will add a default policy:

```csharp
using Dolittle.Resilience;

public class DefaultPolicyDefiner : IDefineDefaultPolicy
{
    public Polly.Policy Define()
    {
        return Polly.Policy.NoOps();
    }
}
```

{{% notice warning %}}
There can only be one definition of a default policy. An exception will be
thrown if you try to add multiple definitions.
{{% /notice %}}

## Usage

To make use of a the default policy, all you need to do is take a dependency to
`IPolicies` in your constructor and get the default policy. Then, the code that
needs resilience around it simply calls the policy's correct `Execute` method.

Below is an example of how to use it:

```csharp
using Dolittle.Resilience;

public class MyResource
{
    IPolicy _policy;

    public MyResource(IPolicies policies)
    {
        _policy = policies.Default;
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
