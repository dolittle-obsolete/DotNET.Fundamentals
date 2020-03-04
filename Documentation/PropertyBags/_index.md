---
title: Property Bags and Object Factory
description: Overview of Property Bag
keywords: Overview, Property Bag
author: smithmx
aliases: 
    - /fundamentals/dotnet.fundamentals/propertybags
    - /fundamentals/dotnet-fundamentals/propertybags
---
## Definition

A *Property Bag* is a read-only, dynamic, dictionary-like structure intended for serializing DTO type entities in a type-independent way.

{{% notice note %}}
Property Bag is not intended as a generic serialization object.  It should only contain DTO-like structures and does not support references.

Its use-case is for separating the persistence of artifacts like Commands and Events from their type representation.
{{% /notice %}}

## Creation

As a Property Bag is read-only after creation, it is populated by passing a *NullFreeDictionary*.  The PropertyBag handles "null" by not having the property.  When rehydrating the object, the property is never set so will have the default value.  It is the responsiblity of the client code to ensure that the default value is valid.

{{% notice note %}}
A null free dictionary is a dictionary that does not allow nulls to be added as keys or values.
{{% /notice %}}

The creation of PropertyBag is best handled through an extension method on Object *ToPropertyBag*.

```csharp
var source = new SimpleDto { String = "hello", Int = 23, DateTime = DateTime.Now };
var result = source.ToPropertyBag();
```

The basic structures of the PropertyBag are primitives, PropertyBags (nested for complex types) and arrays only.

# Object Factory

The *IObjectFactory* is responsible for creating an instance of a type populated with values from the PropertyBag.

The basic interface consists of two simple methods:

```csharp
object Build(Type typeToBuild, PropertyBag source);
T Build<T>(PropertyBag source);
```

# Built in Factories

The ObjectFactory has a number of built-in factories for the construction of different types.

### Immutable types

An immutable type is defined as a type that does not have any properties with setters and has a non-default constructor for populating the values.

{{% notice note %}}
There is a simple convention for instantiating these objects.  The constructor with the most parameters will be chosen.  Where there are multiple constructors with the "most" parameters the selected constructor is not deterministic.  Immutable types
**SHOULD NOT** have multiple overloaded constructors.  They should be simple DTOs with a clear definition of what is required to be populated.

The constructor parameters operate on the basis of a simple convention.  The Property is named in Pascal Case while the constructor parameter is named exactly the same but in Camel Case.
{{% /notice %}}

### Mutable types

A mutable type is defined as a type that has settable properties and which may or may not have a non default constructor.  The same rules for the constructor apply as for the immutable type above.  After construction, the Object Factory will map properties from the PropertyBag to the newly constructed object.

### Concepts

Concepts will be present on the PropertyBag as the underlying Primitive Type.  The Object Factory will create and populate an instance of the Concept when rehydrating the object.

### Property Bags

Complex types (such as Value<> object and DTOs) will be represented upon the Property Bag as a PropertyBag.  The Object Factory can handle nested construction.

### Enumerables

Enumerable types are represented on the PropertyBag as a fixed-size array.  The size being determined by the size of the Enumerable on the source object.  Dictionaries are not supported.

# User Defined Factories

When the user has a complex type that is not handled automatically, they can implement the **IUserDefinedTypeFactory<T>** interface.

```csharp

    /// <summary>
    /// Indicates whether the factory can build the specified type
    /// </summary>
    /// <param name="type">The type to check</param>
    /// <returns>true if it can build, false otherwise</returns>
    bool CanBuild(Type type);
    /// <summary>
    /// Indicates whether the factory can build the specified type
    /// </summary>
    /// <typeparam name="T">The type to check</typeparam>
    /// <returns>true if it can build, false otherwise</returns>
    bool CanBuild<T>();

    /// <summary>
    /// Builds the specific type
    /// </summary>
    /// <param name="type">The type to build</param>
    /// <param name="objectFactory">An instance of <see cref="IObjectFactory" /> to help with building any child types</param>
    /// <param name="source">The instance of <see cref="PropertyBag" /> that is used to populate the instance</param>
    /// <returns>An instance of the type, populated from the <see cref="PropertyBag" /> as an object</returns>
    object Build(Type type, IObjectFactory objectFactory, PropertyBag source);

    /// <summary>
    /// Builds the specific type
    /// </summary>
    /// <typeparam name="T">The type to build</typeparam>
    /// <param name="objectFactory">An instance of <see cref="IObjectFactory" /> to help with building any child types</param>
    /// <param name="source">The instance of <see cref="PropertyBag" /> that is used to populate the instance</param>
    /// <returns>An instance of the type, populated from the <see cref="PropertyBag" /></returns>
    T Build<T>(IObjectFactory objectFactory, PropertyBag source);

```

User defined factory types have the chance to execute before the built-in factories.  This means that a User Defined Factory will take priority over a built in factory and have the first change to indicate that it "Can Build" the type.  There is no mechanism at present for ordering UserDefinedTypeFactory instances.
