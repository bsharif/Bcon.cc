using System.Web.Mvc;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Web.Models;
using NHibernate;

namespace Gecko.Bcon.Web.Controllers {
    /// <summary>
    /// the emergency controller
    /// </summary>
    public class EmergencyController : BaseController {

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmergencyController(ISession dbSession) : base(dbSession) { }

        /// <summary>
        /// The Main Emergency View
        /// </summary>
        public ActionResult Index(string id) {
            var profile = new EmergencyProfileRepository(DBSession).FirstOrDefault(x => x.KeyCode == id);
            if (profile != null) {
                EmergencyProfileViewModel model = new EmergencyProfileViewModel(profile);
                if (profile.User != null) {
                    return View("~/Views/Home/Emergency.cshtml", model);
                } else {
                    return View("~/Views/Account/QuickRegister.cshtml", model);
                }
            }
            return View();
        }
    }
}