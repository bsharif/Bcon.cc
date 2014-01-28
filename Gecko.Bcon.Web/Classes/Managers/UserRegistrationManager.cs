using System.Linq;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Domain;
using Gecko.Bcon.Web.Classes.Commands;
using Gecko.Bcon.Web.Classes.Emails;
using Gecko.Bcon.Web.Classes.Emails.Models;
using NHibernate;

namespace Gecko.Bcon.Web.Classes.Managers {
    public class UserRegistrationManager : ManagerBase {

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbSession"></param>
        public UserRegistrationManager(ISession dbSession) : base(dbSession) { }

        /// <summary>
        /// Register a user with an name, email and plain  text password
        /// </summary>
        public UserRegistrationResult RegisterUser(string name, string email, string password) {
            var cmd = new UserRegistrationCommand(DBSession, name, email, password);
            return cmd.Execute();
        }

        /// <summary>
        /// Send out a confirmation email
        /// </summary>
        public void SendRegistrationConfirmationEmail(string emailAddress, string activationLink) {
            var rep = new UserRepository(DBSession);
            User user = rep.List().First(x => x.Email == emailAddress);
            var model = new RegistrationConfirmationEmailModel(user);
            model.ConfirmationLink = activationLink;
            var email = new RegistrationConfirmationEmail(model);
            email.Send();
        }
    }
}