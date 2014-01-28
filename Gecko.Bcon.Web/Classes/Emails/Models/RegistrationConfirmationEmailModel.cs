
using Gecko.Bcon.Domain;

namespace Gecko.Bcon.Web.Classes.Emails.Models {
    /// <summary>
    /// The registration confirmation URL
    /// </summary>
    public class RegistrationConfirmationEmailModel : EmailModelBase {
        public RegistrationConfirmationEmailModel(User user) {
            ConfirmationLink = user.ActivationCode;
            EmailRecipeints.Add(user.Email);
            Name = user.FirstName + " " + user.LastName;
        }

        /// <summary>
        /// The name used in this email template
        /// </summary>
        public override string TemplateName {
            get { return "RegistrationConfirmationEmail"; }
        }

        /// <summary>
        /// The Subject of this email
        /// </summary>
        public override string Subject {
            get { return "Confirm your email address"; }
        }

        public string ConfirmationLink { get; set; }
        public string Name { get; set; }

    }
}