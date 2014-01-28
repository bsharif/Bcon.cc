using System.Web.Mvc;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Web.Models;
using NHibernate;


namespace Gecko.Bcon.Web.Controllers {

    /// <summary>
    /// The home controller
    /// </summary>
    public class HomeController : BaseController {
        /// <summary>
        /// Default constructor
        /// </summary>
        public HomeController(ISession dbSession) : base(dbSession) {}

        /// <summary>
        /// The main index view
        /// </summary>
        public ActionResult Index() {
            if(User != null && User.Identity.IsAuthenticated) {
                var profile = new EmergencyProfileRepository(DBSession).FirstOrDefault(x => x.User.Id == User.UserId);
                MemberViewModel model = new MemberViewModel(profile);
                return View("MemberIndex", model);
            }
            return View();
        }

        /// <summary>
        /// The About view
        /// </summary>
        public ActionResult About() {
            return View();
        }

        /// <summary>
        /// The Contact View
        /// </summary>
        public ActionResult Contact() {
            return View();
        }

        /// <summary>
        /// The emergency View
        /// </summary>
        public ActionResult Emergency(string id) {
            return RedirectToAction("Index", "Emergency", new {id});
        }

        public ActionResult Credits() {
            return View();
        }
    }
}