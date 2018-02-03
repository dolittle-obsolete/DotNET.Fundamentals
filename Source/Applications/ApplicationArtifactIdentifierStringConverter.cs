/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using doLittle.Artifacts;
using doLittle.Collections;

namespace doLittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationArtifactIdentifierStringConverter"/>
    /// </summary>
    public class ApplicationArtifactIdentifierStringConverter : IApplicationArtifactIdentifierStringConverter
    {
        /// <summary>
        /// The expected format when parsing resources as strings
        /// </summary>
        public static string ExpectedFormat = $"Application{ApplicationSeparator} LocationSegments separated with {ApplicationLocationSeparator} then {ApplicationArtifactSeparator} and resource identifier then {ApplicationArtifactTypeSeparator} and the type. e.g. 'Application{ApplicationSeparator}BoundedContext{ApplicationLocationSeparator}Module{ApplicationLocationSeparator}Feature{ApplicationLocationSeparator}SubFeature{ApplicationArtifactSeparator}Resource{ApplicationArtifactTypeSeparator}Type{ApplicationAreaSeperator}Area'";

        /// <summary>
        /// The separator character used for separating the identification for the <see cref="IApplication"/>
        /// </summary>
        public const char ApplicationSeparator = '#';

        /// <summary>
        /// The separator character used for separating the identification for every <see cref="IApplicationLocation"/> segment
        /// </summary>
        public const char ApplicationLocationSeparator = '.';

        /// <summary>
        /// The separator character used for separating the <see cref="IArtifact"/> from the rest in a string
        /// </summary>
        public const char ApplicationArtifactSeparator = '-';

        /// <summary>
        /// The separator character used for separating the <see cref="IArtifactType"/> from the rest in a string
        /// </summary>
        public const char ApplicationArtifactTypeSeparator = '+';

        /// <summary>
        /// The separator characeter used for separating the <see cref="ApplicationArea"/> from the rest in a string
        /// </summary>
        public const char ApplicationAreaSeperator = '|';

        readonly IApplication _application;
        readonly IArtifactTypes _artifactTypes;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifactIdentifierStringConverter"/>
        /// </summary>
        /// <param name="application">The <see cref="IApplication">application context</see></param>
        /// <param name="artifactTypes"></param>
        public ApplicationArtifactIdentifierStringConverter(IApplication application, IArtifactTypes artifactTypes)
        {
            _application = application;
            _artifactTypes = artifactTypes;
        }

        /// <inheritdoc/>
        public string AsString(IApplicationArtifactIdentifier identifier)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{identifier.Application.Name}{ApplicationSeparator}");
            var first = true;
            identifier.Location.Segments.ForEach(l =>
            {
                if (!first) stringBuilder.Append(ApplicationLocationSeparator);
                first = false;
                stringBuilder.Append($"{l.Name}");
            });
            stringBuilder.Append($"{ApplicationArtifactSeparator}{identifier.Artifact.Name}");
            stringBuilder.Append($"{ApplicationArtifactTypeSeparator}{identifier.Artifact.Type.Identifier}");
            stringBuilder.Append($"{ApplicationAreaSeperator}{identifier.Area}");
            return stringBuilder.ToString();
        }

        /// <inheritdoc/>
        public IApplicationArtifactIdentifier FromString(string identifierAsString)
        {
            ValidateIdentifierString(identifierAsString);

            var regex = new Regex(@"(\w+)#(?:([\w]+)[.]*)+-([\w]+)\+([\w]+)\|([\w]+)");
            var match = regex.Match(identifierAsString);
            ThrowIfFormatIsInvalid(match, identifierAsString);

            var application = match.Groups[1].Value;

            var locationList = new List<string>();
            foreach (Capture capture in match.Groups[2].Captures) locationList.Add(capture.Value);
            var locations = locationList.ToArray();

            var artifactName = match.Groups[3].Value;
            var artifactTypeIdentifier = match.Groups[4].Value;
            var area = match.Groups[5].Value;

            ThrowIfApplicationLocationsMissing(locations, identifierAsString);

            var segments = GetSegmentsFromLocations(locations);

            var artifactType = _artifactTypes.GetFor(artifactTypeIdentifier);
            var artifact = new Artifact(artifactName, artifactType);

            var applicationArtifactIdentifier = new ApplicationArtifactIdentifier(
                _application, 
                area, 
                new ApplicationLocation(segments), 
                artifact);
                
            return applicationArtifactIdentifier;
        }

        void ValidateIdentifierString(string identifierAsString)
        {
            var applicationSeparatorIndex = identifierAsString.IndexOf(ApplicationSeparator);
            ThrowIfApplicationSeparatorMissing(applicationSeparatorIndex, identifierAsString);

            var applicationArtifactSeparatorIndex = identifierAsString.IndexOf(ApplicationArtifactSeparator);
            ThrowIfApplicationArtifactMissing(applicationArtifactSeparatorIndex, identifierAsString);

            var applicationIdentifier = identifierAsString.Substring(0, applicationSeparatorIndex);
            ThrowIfApplicationMismatches(applicationIdentifier, identifierAsString);

            var applicationArtifactTypeSeparatorIndex = identifierAsString.IndexOf(ApplicationArtifactTypeSeparator);
            ThrowIfApplicationArtifactTypeMissing(applicationArtifactTypeSeparatorIndex, identifierAsString);

            var applicationAreaSeparatorIndex = identifierAsString.IndexOf(ApplicationAreaSeperator);
            ThrowIfApplicationAreaMissing(applicationAreaSeparatorIndex, identifierAsString);
        }

        IEnumerable<IApplicationLocationSegment> GetSegmentsFromLocations(string[] locations)
        {
            var fragments = GetFlattenedApplicationStructure(_application.Structure.Root).ToArray();
            var segments = new List<IApplicationLocationSegment>();

            var fragmentIndex = 0;
            IApplicationLocationSegment prevSegment = null;

            for (var index = 0; index < locations.Length; index++)
            {
                if (fragmentIndex >= fragments.Length) break;
                var fragment = fragments[fragmentIndex];

                var constructor = fragment.Type.GetTypeInfo().DeclaredConstructors.First();
                var parameters = constructor.GetParameters();
                var parameterInstances = new List<object>();

                var nameParameter = parameters[0];
                if (parameters.Length == 2)
                {
                    nameParameter = parameters[1];
                    parameterInstances.Add(prevSegment);
                }

                if (nameParameter.ParameterType != typeof(string))
                {
                    var parameterInstance = Activator.CreateInstance(nameParameter.ParameterType);
                    nameParameter.ParameterType.GetProperty("Value").SetValue(parameterInstance, locations[index]);
                    parameterInstances.Add(parameterInstance);
                }
                else parameterInstances.Add(locations[index]);

                var segment = constructor.Invoke(parameterInstances.ToArray()) as IApplicationLocationSegment;
                segments.Add(segment);

                prevSegment = segment;

                if (!fragment.Recursive) fragmentIndex++;
            }

            return segments;
        }

        IEnumerable<IApplicationStructureFragment> GetFlattenedApplicationStructure(IApplicationStructureFragment current, IList<IApplicationStructureFragment> fragments = null)
        {
            if (fragments == null) fragments = new List<IApplicationStructureFragment>();
            fragments.Add(current);
            current.Children.ForEach(child => GetFlattenedApplicationStructure(child, fragments));
            return fragments;
        }

        void ThrowIfApplicationSeparatorMissing(int applicationSeparatorIndex, string identifierAsString)
        {
            if (applicationSeparatorIndex <= 0) throw new UnableToIdentifyApplication(identifierAsString);
        }

        void ThrowIfApplicationArtifactMissing(int applicationResourceSeparatorIndex, string identifierAsString)
        {
            if (applicationResourceSeparatorIndex <= 0) throw new MissingApplicationArtifact(identifierAsString);
        }

        void ThrowIfApplicationLocationsMissing(string[] locations, string identifierAsString)
        {
            if (locations.Length == 0) throw new MissingApplicationLocations(identifierAsString);
        }

        void ThrowIfApplicationMismatches(string applicationIdentifier, string identifierAsString)
        {
            if (_application.Name != applicationIdentifier) throw new ApplicationMismatch(_application.Name, identifierAsString);
        }

        void ThrowIfApplicationArtifactTypeMissing(int applicationArtifactTypeSeparatorIndex, string identifierAsString)
        {
            if (applicationArtifactTypeSeparatorIndex <= 0) throw new MissingApplicationArtifactType(identifierAsString);
        }

        void ThrowIfApplicationAreaMissing(int applicationAreaSeparatorIndex, string identifierAsString)
        {
            if (applicationAreaSeparatorIndex <= 0) throw new MissingApplicationArea(identifierAsString);
        }

        void ThrowIfFormatIsInvalid(Match match, string identifierAsString)
        {
            if (!match.Success) throw new InvalidApplicationArtifactIdentifierFormat(identifierAsString);
        }
    }
}