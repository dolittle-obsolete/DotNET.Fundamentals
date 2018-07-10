using Dolittle.Artifacts;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifier.given
{
    public class MyArtifactType : IArtifactType
    {
        public string Identifier {get; }

        public MyArtifactType(string identifier)
        {
            Identifier = identifier;
        }
    }
}