using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Jimmy_Waern_Assignment2.Data
{
    public class MinimumAgeRequirement : AuthorizationHandler<MinimumAgeRequirement>, IAuthorizationRequirement
    {
        int _minimumAge;

        public MinimumAgeRequirement(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "MinimumAge"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var age = int.Parse(context.User.FindFirst(c => c.Type == "MinimumAge").Value);

            if (age >= requirement._minimumAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
