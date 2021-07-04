using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Analysis.Models;

namespace Analysis.Infrastructure
{
    [HtmlTargetElement("td",Attributes ="is-admin")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public RoleUsersTagHelper(UserManager<IdentityUser> usermgr,
        RoleManager<IdentityRole> rolemgr)
        {
            userManager = usermgr;
            roleManager = rolemgr;
        }

        [HtmlAttributeName("is-admin")]
        public string UserId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            IdentityUser user = await userManager.FindByIdAsync(UserId);
            if(user != null && await userManager.IsInRoleAsync(user, "Admins"))
            {
                output.Content.SetContent("True");
            }
            else
            {
                output.Content.SetContent("False");
            }
        }
    }
}
