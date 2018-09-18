using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Dolittle.Collections;
using Machine.Specifications;

namespace Security.for_ClaimsPrincipalExtensions.given
{
    public class a_claims_principal
    {
        public static IEnumerable<Dolittle.Security.Claim> dolittle_claims;
        public static IEnumerable<System.Security.Claims.Claim> dotnet_claims;
        public static ClaimsPrincipal instance;

        Establish context = () => 
        {
            dotnet_claims =  new List<System.Security.Claims.Claim>
            {
                new Claim(ClaimTypes.Name, "Michael")
                , new Claim(ClaimTypes.Country, "Norway")
                , new Claim(ClaimTypes.Gender, "M")
                , new Claim(ClaimTypes.Surname, "Smith")
                , new Claim(ClaimTypes.Email, "michael@dolittle.com")
                , new Claim(ClaimTypes.Role, "Coffee Maker")
            };

            dolittle_claims = dotnet_claims.Select(c => Dolittle.Security.Claim.FromDotnetClaim(c));

            instance = new ClaimsPrincipal(new ClaimsIdentity(dotnet_claims));
        };
    }
}