using FluentValidation;

namespace Gecko.Bcon.Web.Models.Validators {
    /// <summary>
    /// Basic validation rules for the creation of a profile
    /// </summary>
    public class CreateProfileViewModelValidator : AbstractValidator<CreateProfileViewModel> {
        public CreateProfileViewModelValidator() {
            RuleFor(x => x.FirstName).NotEmpty().Length(0, Common.Constants.DEFAULT_TEXT_STRING_SIZE);
            RuleFor(x => x.LastName).Length(0, Common.Constants.DEFAULT_TEXT_STRING_SIZE);

            RuleFor(x => x.EmergencyContactName).NotEmpty().Length(0, Common.Constants.DEFAULT_TEXT_STRING_SIZE); 
            RuleFor(x => x.EmergencyContanctNumber).NotEmpty().Length(0, Common.Constants.DEFAULT_TEXT_STRING_SIZE);

            RuleFor(x => x.NextOfKinName).Length(0, Common.Constants.DEFAULT_TEXT_STRING_SIZE);
            RuleFor(x => x.NextOfKinNumber).Length(0, Common.Constants.DEFAULT_TEXT_STRING_SIZE);
            RuleFor(x => x.NHSNumber).Length(0, Common.Constants.DEFAULT_TEXT_STRING_SIZE);


            RuleFor(x => x.Conditions).Length(0, Common.Constants.MAX_DBTEXT_STRING_SIZE);
            RuleFor(x => x.Allergies).Length(0, Common.Constants.MAX_DBTEXT_STRING_SIZE);
            RuleFor(x => x.Medication).Length(0, Common.Constants.MAX_DBTEXT_STRING_SIZE);

            
        }
    }
}