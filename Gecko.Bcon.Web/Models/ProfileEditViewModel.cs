using System;
using Gecko.Bcon.Domain;
using Gecko.Bcon.Web.Models;

namespace Gecko.Bcon.Web.Models {
    public class ProfileEditViewModel : BaseViewModel {

        public ProfileEditViewModel() {
        }
        public ProfileEditViewModel(EmergencyProfile profile) {
            NextOfKinName = profile.NextOfKinName;
            NextOfKinNumber = profile.NextOfKinNumber;
            EmergencyContactName  = profile.EmergencyContact;
            EmergencyContanctNumber = profile.EmergencyNumber;
            Allergies = profile.Allergies;
            Medication = profile.Medication;
            Conditions = profile.Conditions;
            FirstName = profile.User.FirstName;
            LastName = profile.User.LastName;
            NHSNumber = profile.NHSNumber;
            if(profile.DOB.HasValue) {
                DateOfBirth = profile.DOB.Value.ToShortDateString();
            }
            ProfilePhoto = profile.User.ProfileImage;
        }

        public string ProfilePhoto { get; set; }

        public string EmergencyContactName { get; set; }
        public bool DeleteProfileImage { get; set; }
        public string EmergencyContanctNumber { get; set; }
        public string Allergies { get; set; }
        public string Medication { get; set; }
        public string Conditions { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NextOfKinNumber { get; set; }
        public string NextOfKinName { get; set; }
        public string NHSNumber { get; set; }
        public string DateOfBirth { get; set; }
    }
}