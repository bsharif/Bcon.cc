using Gecko.Bcon.Web.Classes.Emails.Models;

namespace Gecko.Bcon.Web.Classes.Emails {
    /// <summary>
    /// The registration confirmation email
    /// </summary>
    public class RegistrationConfirmationEmail : EmailBase<RegistrationConfirmationEmailModel> {
        public RegistrationConfirmationEmail(RegistrationConfirmationEmailModel model)
            : base(model) { }
    }
}