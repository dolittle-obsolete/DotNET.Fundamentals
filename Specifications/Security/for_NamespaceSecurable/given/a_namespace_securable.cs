using doLittle.Security;

namespace doLittle.Security.Specs.for_NamespaceSecurable.given
{
    public class a_namespace_securable
    {
        protected static NamespaceSecurable namespace_securable;
        protected static object action_with_namespace_match;
        protected static object action_within_another_namespace;

        public a_namespace_securable()
        {
            action_with_namespace_match = new SomeType();
            action_within_another_namespace = new DifferentNamespace.TypeInDifferentNamespace();

            namespace_securable = new NamespaceSecurable(typeof(SomeType).Namespace);
        }
    }
}