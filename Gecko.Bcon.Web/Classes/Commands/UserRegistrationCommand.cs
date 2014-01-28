using System;
using System.Linq;
using Gecko.Bcon.Common;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Domain;
using NHibernate;

namespace Gecko.Bcon.Web.Classes.Commands {
    public class UserRegistrationCommand : BaseCommand {
        private readonly ISession _session;
        private readonly string _email;
        private readonly string _password;
        private readonly string _name;
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserRegistrationCommand(ISession dbSession, string name, string email, string password) {
            _session = dbSession;
            _email = email.Trim();
            _password = password;
            _name = name;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <returns></returns>
        public UserRegistrationResult Execute() {
            var registrationResult = new UserRegistrationResult();

            try {
                //Check if email exists
                var rep = new UserRepository(_session);
                User user = rep.List().FirstOrDefault(x => x.Email == _email);
                if (user == null) {
                    user = new User();
                    user.Email = _email;
                    user.FirstName = _name;
                    user.CreatedOn = DateTime.UtcNow;
                    user.Role = User.Roles.Registered;
                    _session.Save(user);
                    _session.Flush();
                    user.Password = CryptoLibrary.GetHash(user.Id.ToString() + _password);
                    user.ActivationCode = CryptoLibrary.GetHash(user.Password).Substring(2, 11);
                    _session.Save(user);
                    registrationResult.Result.Successful = true;
                    registrationResult.User = user;
                } else {
                    registrationResult.Result.Successful = false;
                    registrationResult.Result.Message = "Email account already exists";
                }
            } catch (Exception e) {
                registrationResult.Result.Message = e.Message;
                registrationResult.Result.Successful = false;
            }
            return registrationResult;
        }
    }
}