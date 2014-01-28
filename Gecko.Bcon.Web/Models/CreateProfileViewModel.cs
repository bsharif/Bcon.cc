using System;
using FluentValidation.Attributes;
using Gecko.Bcon.Web.Models.Validators;

namespace Gecko.Bcon.Web.Models {
    [Validator(typeof(CreateProfileViewModelValidator))]
    public class CreateProfileViewModel : BaseViewModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContanctNumber { get; set; }
        public string Allergies { get; set; }
        public string Medication { get; set; }
        public string Conditions { get; set; }
        public string NextOfKinNumber { get; set; }
        public string NextOfKinName { get; set; }
        public string NHSNumber { get; set; }
        public string DateOfBirth { get; set; }
        public virtual String ProfilePhoto { get; set; }
    }
}