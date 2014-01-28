using System;
using Gecko.Bcon.Domain;

namespace Gecko.Bcon.Web.Models {
    public class EmergencyProfileViewModel : BaseViewModel {
        public EmergencyProfileViewModel(EmergencyProfile profile) {
            NextOfKinName = profile.NextOfKinName;
            NextOfKinNumber = profile.NextOfKinNumber;
            EmergencyContact = profile.EmergencyContact;
            EmergencyNumber = profile.EmergencyNumber;
            Name = profile.User.FirstName + " " + profile.User.LastName;
            Allergies = profile.Allergies;
            Medication = profile.Medication;
            Conditions = profile.Conditions;
            ProfileImage = profile.User.ProfileImage;
            DOB = profile.DOB;
            NHSNumber = profile.NHSNumber;
            if (DOB.HasValue) {
                DateTime today = DateTime.UtcNow;
                Age = today.Year - DOB.Value.Year;
                if (DOB.Value > today.AddYears(-Age)) {
                    Age--;
                }
            }
            KeyCode = profile.KeyCode;
            if (profile.LastUpdated.HasValue) {
                ShowUpdatedWarning = profile.LastUpdated.Value.AddDays(35) < DateTime.UtcNow;
            }
        }

        public bool ShowUpdatedWarning { get; set; }
        public string EmergencyNumber { get; set; }
        public string EmergencyContact { get; set; }
        public string ProfileImage { get; set; }
        public string KeyCode { get; set; }
        public string NextOfKinNumber { get; set; }
        public string NextOfKinName { get; set; }
        public string NHSNumber { get; set; }
        public string Name { get; set; }
        public string Conditions { get; set; }
        public string Medication { get; set; }
        public string Allergies { get; set; }
        public DateTime? DOB { get; set; }
        public int Age { get; set; }
    }
}