using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Domain;
using Gecko.Bcon.Web.Classes.Managers;
using Gecko.Bcon.Web.Models;
using NHibernate;

namespace Gecko.Bcon.Web.Controllers {
    [Authorize]
    public class ProfileController : BaseController {
        public ProfileController(ISession dbSession) : base(dbSession) {}

        /// <summary>
        /// Display for consent form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Consent()
        {
            return View();
        }

        /// <summary>
        /// Display for consent form
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Consent(ConsentViewModel model )
        {
            if(model.Consent)
            {
                return RedirectToAction("Create");
            }
            base.Error("Without your consent to store your data, we are unable to create your Bcon Profile. If you have specific concerns please contact us");
            return View();
        }

        /// <summary>
        /// Creates a profile if you don't have one
        /// </summary>
        [HttpGet]
        public ActionResult Create() {
            EmergencyProfile profile =
                new EmergencyProfileRepository(DBSession).FirstOrDefault(x => x.User.Id == User.UserId);
            if (profile == null) {
                return View(new CreateProfileViewModel());
            } else {
                return RedirectToAction("Update");
            }
        }

        /// <summary>
        /// The actual create profile page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreateProfileViewModel model, HttpPostedFileBase profilePhoto)
        {
            if (ModelState.IsValid) {
                var manager = new EmergencyProfileManager(DBSession);
                EmergencyProfile profile = manager.Save(model.NextOfKinName,
                                                        model.NextOfKinNumber,
                                                        model.EmergencyContactName,
                                                        model.EmergencyContanctNumber,
                                                        model.NHSNumber,
                                                        model.DateOfBirth,
                                                        model.Allergies,
                                                        model.Medication,
                                                        model.Conditions,
                                                        User.UserId
                    );
        

                string url = GetShortenedUrl(Url.Action("Emergency", "Home", new {id = profile.KeyCode}, "http"));
                profile.Url = url;
                string imageName = string.Empty;
                if (profilePhoto != null)
                {
                    WebImage img = new WebImage(profilePhoto.InputStream);
                    if (img.Width > 200 || img.Height > 200)
                    {
                        img.Resize(200, 200, true);
                    }
                    imageName = User.Email.GetHashCode() + Path.GetExtension(profilePhoto.FileName);
                    string path = Server.MapPath("/UserContent/" + imageName);

                    img.Save(path);
                }
                var userManager = new UserManager(DBSession);
                userManager.Update(model.FirstName, model.LastName,imageName,  User.UserId);
                DBSession.Save(profile);
                return RedirectToAction("Details");
            }
            return View(model);
        }

        /// <summary>
        /// Generates a small url, so QR code is not so big
        /// </summary>
        /// <param name="url">the url to shorten</param>
        /// <returns>shortened url</returns>
        private string GetShortenedUrl(string url) {
            string googReturnedJson = string.Empty;
            var javascriptSerializer = new JavaScriptSerializer();

            var googSentJson = new GoogleShortenedURLRequest();
            googSentJson.longUrl = url;
            string jsonData = javascriptSerializer.Serialize(googSentJson);

            byte[] bytebuffer = Encoding.UTF8.GetBytes(jsonData);
            WebRequest webreq =
                WebRequest.Create("https://www.googleapis.com/urlshortener/v1/url?key=" +
                                  ConfigurationManager.AppSettings.Get("GoogleAPIKey"));
            webreq.Method = WebRequestMethods.Http.Post;
            webreq.ContentLength = bytebuffer.Length;
            webreq.ContentType = "application/json";

            using (Stream stream = webreq.GetRequestStream()) {
                stream.Write(bytebuffer, 0, bytebuffer.Length);
                stream.Close();
            }

            using (var webresp = (HttpWebResponse) webreq.GetResponse())
            using (Stream dataStream = webresp.GetResponseStream())
            using (var reader = new StreamReader(dataStream)) googReturnedJson = reader.ReadToEnd();

            var googUrl = javascriptSerializer.Deserialize<GoogleShortenedURLResponse>(googReturnedJson);

            return googUrl.id;
        }

        /// <summary>
        /// Displays the details of your profile
        /// </summary>
        /// <returns></returns>
        public ActionResult Details() {
            EmergencyProfile profile = new EmergencyProfileRepository(DBSession).FirstOrDefault(x => x.User.Id == User.UserId);
            if (profile == null) return RedirectToAction("Consent");
            var model = new ProfileDetailViewModel(profile);
            return View(model);
        }

        /// <summary>
        /// Updates a profile
        /// </summary>
        [HttpGet]
        public ActionResult Update() {
            EmergencyProfile profile = new EmergencyProfileRepository(DBSession).First(x => x.User.Id == User.UserId);
            var model = new ProfileEditViewModel(profile);
            return View(model);
        }


        /// <summary>
        /// Update a profile
        /// </summary>
        [HttpPost]
        public ActionResult Update(ProfileEditViewModel model, HttpPostedFileBase profilePhoto)
        {
            if(ModelState.IsValid) {
                var manager = new EmergencyProfileManager(DBSession);
                manager.Save(model.NextOfKinName,
                    model.NextOfKinNumber,
                    model.EmergencyContactName,
                            model.EmergencyContanctNumber, model.NHSNumber, model.DateOfBirth,
  
                            model.Allergies, 
                            model.Medication, 
                            model.Conditions,
                            User.UserId);
                string imageName = string.Empty;
                var userManager = new UserManager(DBSession);
                if (profilePhoto != null )
                {
                    WebImage img = new WebImage(profilePhoto.InputStream);
                    if (img.Width > 200 || img.Height > 200)
                    {
                        img.Resize(200, 200, true);
                    }
                    imageName = User.Email.GetHashCode() + Path.GetExtension(profilePhoto.FileName);
                    string path = Server.MapPath("/UserContent/" + imageName);

                    img.Save(path);
                } else
                {
                    if (!model.DeleteProfileImage)
                    {
                        var user = new UserRepository(DBSession).GetById(User.UserId);
                        imageName = user.ProfileImage;
                    }
                }
                
                userManager.Update(model.FirstName, model.LastName, imageName, User.UserId);
                Information("Profile updated");
                return RedirectToAction("Details");
            }
            return View(model);
        }

        /// <summary>
        /// Gets the delete view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete() {
            return View();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="confirm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string confirm) {
            var rep = new EmergencyProfileRepository(DBSession);
            rep.DeleteProfileFor(User.UserId);
            Information("Your profile has been deleted");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Json result mapped to dto
        /// </summary>
        private class GoogleShortenedURLResponse {
            public string id { get; set; }
        }


        /// <summary>
        /// Json result mapped to dto
        /// </summary>
        private class GoogleShortenedURLRequest {
            public string longUrl { get; set; }
        }
    }
}