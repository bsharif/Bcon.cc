using System;
using Gecko.Bcon.Domain;
using NHibernate;

namespace Gecko.Bcon.DataAccess.Repositories
{

    /// <summary>
    /// The Basic User Repository
    /// </summary>
    public class UserRepository : RepositoryBase<User>
    {
        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="session">nHibernate session</param>
        public UserRepository(ISession session) : base(session) { }
    }

    /// <summary>
    /// The EmergencyProfile Repository
    /// </summary>
    public class EmergencyProfileRepository : RepositoryBase<EmergencyProfile> {
        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="session">nHibernate session</param>
        public EmergencyProfileRepository(ISession session) : base(session) {}

        /// <summary>
        /// Delete a profile, we can not delete fully  as QR code has been used.
        /// Need to prevent reuse - keycode must remain.
        /// </summary>
        /// <param name="userId">The user id of the profile to delete</param>
        public void DeleteProfileFor(long userId) {
            var profile = FirstOrDefault(x => x.User.Id == userId);
            if (profile != null) {
                profile.User = null; //remove user link
                profile.DateCreated = null;
                profile.LastUpdated = null;
                profile.Allergies = string.Empty;
                profile.Conditions = string.Empty;
                profile.DOB = null;
                profile.EmergencyContact = string.Empty;
                profile.EmergencyNumber = string.Empty; 
                profile.Medication = string.Empty; 
                profile.NHSNumber = string.Empty; 
                profile.NextOfKinName = string.Empty; 
                profile.NextOfKinNumber = string.Empty; 
                profile.Url = string.Empty; 
            }
            _session.Save(profile);
        }
    }



}