using System.Linq;
using Dolittle.DependencyInversion;
using Dolittle.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Security.Specs.for_SecurableExtensions
{
    public class when_specifying_user
    {
        static TypeSecurable securable;
        static ISecurityActor actor;
        static ICanResolvePrincipal principalResolver;

        Establish context = () => 
        {
            securable = new TypeSecurable(typeof(object));
            principalResolver = new Mock<ICanResolvePrincipal>().Object;
        };

        Because of = () => actor = securable.UserFrom(principalResolver);

        It should_return_an_user_actor_builder = () => actor.ShouldBeOfExactType<UserSecurityActor>();
        It should_add_actor_to_securable = () => securable.Actors.First().ShouldBeOfExactType<UserSecurityActor>();
    }
}
