using System;
using System.Linq;
using System.Web.Mvc;
using Gecko.Bcon.Common;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Domain;
using Gecko.Bcon.Web.Classes.Commands;
using NHibernate;

namespace Gecko.Bcon.Web.Classes.Managers {
    public class UserManager : ManagerBase {

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="session">nHibernate session</param>
        public UserManager(ISession session) : base(session) { }

        /// <summary>
        /// Try to log in a user
        /// </summary>
        public UserLoginResult LogIn(string email, string password, out User user) {
            user = new UserRepository(DBSession)
                .List().FirstOrDefault(x => x.Email == email);
            var result = new UserLoginResult();

            if (user == null) {
                result.Result.Message = "Account details not found. check user name and password";
                result.Result.Successful = false;
                return result;
            }
            if (user.Password == CryptoLibrary.GetHash(user.Id.ToString() + password)) {
                switch (user.State) {
                    case User.States.PendingActivation:
                        result.Result.Message = "Please activate your account";
                        break;
                    case User.States.Active:
                        result.Result.Successful = true;
                        break;
                    case User.States.Disabled:
                        result.Result.Message = "Account disabled";
                        break;
                    case User.States.Deactivated:
                        result.Result.Message = "Account expired";
                        break;
                    case User.States.PendingDeletion:
                        result.Result.Message = "Account no longer exists";
                        break;
                    default:
                        result.Result.Message = "Account details not found. check user name and password";
                        result.Result.Successful = false;
                        break;
                }
            } else {
                result.Result.Message = "Account details not found. check user name and password";
                result.Result.Successful = false;
            }

            return result;
        }

        /// <summary>
        /// Base result from a log in 
        /// </summary>
        public class UserLoginResult : BaseCommand { }

        /// <summary>
        /// Updates the user first name and last name
        /// </summary>
        public ManagerResult Update(string firstName, string lastName, string imageName, long userId) {
            var result = new ManagerResult();
            try {
                var user = new UserRepository(DBSession).GetById(userId);
                user.FirstName = firstName;
                user.LastName = lastName;
                user.ProfileImage = imageName;
                DBSession.Save(user);
                result.Successful = true;
            } catch(Exception e) {
                result.Successful = false;
                result.Message = e.Message;
            }
            return result;
        }
    }
}