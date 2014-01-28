using System.Linq;
using System.Web.Mvc;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Domain;
using Gecko.Bcon.Web.Areas.Admin.Models;
using MvcContrib.Pagination;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using NHibernate;

namespace Gecko.Bcon.Web.Areas.Admin.Controllers {
    /// <summary>
    /// The home controller for site Admins
    /// </summary>
    public class HomeController : BaseAdminController {
        /// <summary>
        /// DEfault constructor
        /// </summary>
        public HomeController(ISession dbSession) : base(dbSession) {}

        /// <summary>
        /// Gets the index view
        /// </summary>
        public ActionResult Index() {
            return RedirectToAction("Dashboard");
        }

        /// <summary>
        /// Gets the dashboard
        /// </summary>
        /// <returns></returns>
        public ActionResult Dashboard() {
            return View();
        }

        /// <summary>
        /// The orders
        /// </summary>
        public ActionResult Orders() {
            return View();
        }

        /// <summary>
        /// Users list view 
        /// </summary>
        public ActionResult Users(int? page, GridSortOptions sortOptions, string keyword) {
            var model = new UserGridViewModel();
            IQueryable<User> users = new UserRepository(DBSession).List();
            model.SortOptions = sortOptions;
            if (sortOptions.Column != null) {
                users = users.OrderBy(sortOptions.Column, sortOptions.Direction);
            } else {
                users = users.OrderByDescending(x => x.Id);
            }
            if (keyword != null) {
                users =
                    users.Where(
                        x => x.FirstName.Contains(keyword) || x.LastName.Contains(keyword) || x.Email.Contains(keyword));
            }
            model.Results = users.AsPagination(page ?? 1, 20);
            return View(model);
        }

    }
}