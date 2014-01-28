using System;
using System.Web.Mvc;

namespace Gecko.Bcon.Web.Classes.Attributes {
    /// <summary>
    /// Authorize a user by their role
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute {

        public Domain.User.Roles UserRole { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext) {
            Roles = UserRole.ToString();
            base.OnAuthorization(filterContext);
        }

    }
}