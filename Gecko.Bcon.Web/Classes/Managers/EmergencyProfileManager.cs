using System;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Domain;
using NHibernate;

namespace Gecko.Bcon.Web.Classes.Managers {
    /// <summary>
    /// MAnager for the emergency profile
    /// </summary>
    public class EmergencyProfileManager : ManagerBase {

        /// <summary>
        /// Trys to generate a unique profile key
        /// </summary>
        private string GenerateProfileKey(int size) {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyz-0123456789_ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strPwd = "";
            Random rnd = new Random(Environment.TickCount);
            for (int i = 0; i <= size; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1);
            }
            return strPwd;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="session">nHibernate session</param>
        public EmergencyProfileManager(ISession session) : base(session) {}

        /// <summary>
        /// Saves an emergency profile
        /// </summary>
        public EmergencyProfile Save(string nextOfKinName, string nextOfKinNumber,
                         string emergencyContactName, string emergencyContanctNumber, 
            string nhsNumber, string dateOfBirth,string allergies, string medication, string conditions, long userId)
        {


            EmergencyProfile profile = new EmergencyProfileRepository(DBSession).FirstOrDefault(x => x.User.Id == userId);

            if (profile == null) {
                profile = new EmergencyProfile();
                int keySize = 12;
                string key = GenerateProfileKey(keySize);
                while(true) {
                    key = GenerateProfileKey(keySize);
                    var existingProfile = new EmergencyProfileRepository(DBSession).FirstOrDefault(x => x.KeyCode == key);
                    if (existingProfile == null) break;
                    keySize++;
                    if (keySize > 22) {
                        throw new Exception("Out of profile Keys");
                    }
                }
                var user = new UserRepository(DBSession).GetById(userId);
                profile.KeyCode = key;
                profile.User = user;
                profile.DateCreated = DateTime.UtcNow;
            }
            profile.NextOfKinName = nextOfKinName;
            profile.NextOfKinNumber = nextOfKinNumber;
            profile.LastUpdated = DateTime.UtcNow;
            profile.NHSNumber = nhsNumber;

            DateTime? dob = null;
            if (!string.IsNullOrEmpty(dateOfBirth)) {
                try {
                    dob = DateTime.Parse(dateOfBirth);
                }
                catch (Exception) {}
            }
            profile.DOB = dob;
            profile.EmergencyContact = emergencyContactName;
            profile.EmergencyNumber = emergencyContanctNumber;
            profile.Allergies = allergies;
            profile.Medication = medication;
            profile.Conditions = conditions;
            DBSession.Save(profile);
            return profile;
        }
    }
}

