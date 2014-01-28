using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Domain;
using Gecko.Bcon.Web.Classes;
using Gecko.Bcon.Web.Classes.Attributes;
using Gecko.Bcon.Web.Classes.Commands;
using Gecko.Bcon.Web.Classes.Managers;
using Gecko.Bcon.Web.Models;
using NHibernate;

namespace Gecko.Bcon.Web.Controllers {

    /// <summary>
    /// The main account controller
    /// </summary>
    public class AccountController : BaseController {

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbSession">A database session</param>
        public AccountController(ISession dbSession) : base(dbSession) {}

        /// <summary>
        /// Get the log in View
        /// </summary>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, bool checkEmail = false, bool activated = false) {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.CheckEmail = checkEmail;
            ViewBag.Activated = activated;
            return View();
        }

        /// <summary>
        /// Gets the view for your Bcon
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult MyBCon() {
            EmergencyProfile profile = new EmergencyProfileRepository(DBSession).FirstOrDefault(x => x.User.Id == User.UserId);
            if (profile == null) return RedirectToAction("Consent", "Profile");
            var model = new MyMediGramViewModel();
            model.Load(profile);
            return View(model);
        }

        /// <summary>
        /// Post to the log in 
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl, bool checkEmail = false, bool activated = false) {
            ViewBag.CheckEmail = checkEmail;
            ViewBag.Activated = activated;

            if (ModelState.IsValid) {
                User user;
                var manager = new UserManager(DBSession);
                UserManager.UserLoginResult loginResult = manager.LogIn(model.UserName, model.Password, out user);

                if (loginResult.Result.Successful) {
                    PrepareAuthentication(user);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\")) {
                        return Redirect(returnUrl);
                    } else {
                        return RedirectToAction("Index", "Home");
                    }
                } else {
                    ModelState.AddModelError("", loginResult.Result.Message);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Prepares a user for logging in
        /// </summary>
        private void PrepareAuthentication(User user) {
            var serializeModel = new CustomPrincipalSerializeModel {
                                                                       Email = user.Email,
                                                                       Name = user.FirstName + " " + user.LastName,
                                                                       UserId = user.Id,
                                                                       UserRole = user.Role
                                                                   };


            string userData = new JavaScriptSerializer().Serialize(serializeModel);

            var authTicket = new FormsAuthenticationTicket(
                1,
                user.Email, //user id
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(40), // expiry
                true, //do not remember
                userData,
                "/");
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                        FormsAuthentication.Encrypt(authTicket));
            Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Logs off
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff() {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// gets the register view 
        /// </summary>
        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        /// <summary>
        /// The actual registration of  a User
        /// </summary>
        [HttpPost]
        public ActionResult Register(RegisterModel model) {

            if (ModelState.IsValid) {
                // Attempt to register the user
                var manager = new UserRegistrationManager(base.DBSession);
                UserRegistrationResult registrationResult = manager.RegisterUser(model.UserName, model.UserName,
                                                                                 model.Password);
                if (registrationResult.Result.Successful) {
                    string url = Url.Action("Activate", "Account", new {registrationResult.User.ActivationCode},
                                            Request.Url.Scheme);
                    manager.SendRegistrationConfirmationEmail(model.UserName, url);
                    return RedirectToAction("Login", "Account", new {CheckEmail = true});
                } else {
                    ModelState.AddModelError("", registrationResult.Result.Message);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Activate an account with an activation code
        /// </summary>
        public ActionResult Activate(string activationCode) {
            User user = new UserRepository(DBSession).List().FirstOrDefault(x => x.ActivationCode == activationCode);
            if (user != null) {
                if (user.State == Domain.User.States.PendingActivation) {
                    user.State = Domain.User.States.Active;
                    user.ActivationCode = "";
                    DBSession.Save(user);
                    return RedirectToAction("Login", "Account", new {Activated = true});
                }
            }
            ViewBag.Message = "An error occurred during activation. This account has already been activated";
            return RedirectToAction("Login", "Account");
        }
    }
}