using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var ageClaim = context.User.FindFirst(c => c.Type == "Age");

        if (ageClaim == null)
        {
            return Task.CompletedTask; // No age claim, fail authorization
        }

        if (int.TryParse(ageClaim.Value, out int age) && age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
