using Gecko.Bcon.Web.Classes.Attributes;
using Gecko.Bcon.Web.Controllers;
using NHibernate;

namespace Gecko.Bcon.Web.Areas.Admin.Controllers {
    [AuthorizeRole(UserRole = Domain.User.Roles.Admin)]
    public abstract  class BaseAdminController :BaseController  {
        protected BaseAdminController(ISession dbSession) : base(dbSession) {}
    }
}