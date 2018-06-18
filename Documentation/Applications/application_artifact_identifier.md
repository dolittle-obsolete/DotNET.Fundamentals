# Application Artifact Identifier

Applications hold different code artifacts. To be able to identify these, we have the `ApplicationArtifactIdentifier`.
It represents these artifacts in a programming language / runtime agnostic manner.
The representation takes into account the location within an application, which area of interest (often referred to as tier),
what type of artifact it is and which generation of the specific type it represents.
The reason for this representation is to be able to identify the artifacts in an agnostic way, enabling an artifact to be
represented in multiple programming languages and runtimes for different purposes. In some cases the identifier gets
stored and with an identifier representing the logical location within the application, the artifact can be moved within
the language specific structure - such as namespaces in C#, or even move between languages.

## Application

At the heart sits the application the artifact belongs to. The application is represented with a name and also holds the
allowed structure that can be used to in a language specific manner map between.

## Application Structure

The applications allowed structure is defined by the `ApplicationStructure`. It is the construct that describes how an application
is configured and what is expected and allowed to define it. An `ApplicationArtifactIdentifier` must abide by the rules set
in the structure.

### Application Structure Fragment

The structure consists of fragments that defines which location segment is expected at the different levels.
It also describes if it is optional and/or it is recursive.

## Application Area

Typically in applications one have different technical areas, or tiers. These are represented as an area - a string.

## Application Location

The actual location within an application is built from different segments. The location is basically just a path
to the artifact within the application.

### Application Location Segment Name

Each location segment has a name that represents it.

### Application Location Segment

The location path consists of segments, a location is a collection of these segments. There are different types of
segments.

#### Bounded Context

The bounded context is a non-optional segment, every application has at least one bounded context.

#### Module

Within a bounded context there could be one or more modules. This is optional.

#### Feature

Within a bounded context or a module, one can have features.

#### Sub Feature

Within a feature there can be more specific sub features.
