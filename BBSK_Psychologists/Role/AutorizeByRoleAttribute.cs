using BBSK_Psycho.DataLayer.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BBSK_Psycho
{
    public class AuthorizeByRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeByRoleAttribute(params Role[] roles)
        {

            Roles = string.Join(",", roles);
            Roles += $",{Role.Manager}";

        }
    }

}
