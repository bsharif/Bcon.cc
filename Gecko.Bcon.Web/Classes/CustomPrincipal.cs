using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using Gecko.Bcon.Domain;

namespace Gecko.Bcon.Web.Classes {
    
    /// <summary>
    /// Custom principle for this application
    /// http://stackoverflow.com/questions/1064271/asp-net-mvc-set-custom-iidentity-or-iprincipal
    /// </summary>
    public class CustomPrincipal : ICustomPrincipal {
        private string _roles;

        public CustomPrincipal(FormsIdentity identity, CustomPrincipalSerializeModel model)
            : this(identity) {
            UserId = model.UserId;
            Name = model.Name;
            Email = model.Email;
            _roles = model.UserRole.ToString().ToLower();
        }

        public CustomPrincipal(IIdentity identity) {
            Identity = identity;
        }

        public bool IsInRole(string role) {
            return _roles.Contains(role.ToLower());
        }

        public IIdentity Identity { get; private set; }

        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }

    public class CustomPrincipalSerializeModel {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public User.Roles UserRole { get; set; }
    }

    public interface ICustomPrincipal : IPrincipal {
        long UserId { get; set; }
        string Email { get; set; }
    }
    public abstract class BaseViewPage : WebViewPage {
        public virtual new CustomPrincipal User {
            get { return base.User as CustomPrincipal; }
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel> {
        public virtual new CustomPrincipal User {
            get { return base.User as CustomPrincipal; }
        }
    }
}