using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WLoveAnimals.Pages.Authorization
{
    public class AdminRequirement : IAuthorizationRequirement
    {
        public AdminRequirement(int useraccess)
        {
            UserAccess = useraccess;
        }
        public int UserAccess { get; }
    
    /*public class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "AccountDate"))
                return Task.CompletedTask;

            var accDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "AccountDate").Value);
            var period = DateTime.Now - accDate;  //implementarea perioadei de acces
            if (period.Days > 30 * requirement.UserAccess)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }*/
}

}
