using System.Collections.Generic;

namespace Gecko.Bcon.Web.Classes.Emails.Models {
    /// <summary>
    /// Base class for creating new emails
    /// </summary>
    public abstract class EmailModelBase {
        public EmailModelBase() {
            EmailRecipeints = new List<string>();
        }
        /// <summary>
        /// The email template name
        /// </summary>
        public abstract string TemplateName { get; }

        /// <summary>
        /// The email subject name
        /// </summary>
        public abstract string Subject { get; }
        /// <summary>
        /// The persons the email is going to
        /// </summary>
        public List<string> EmailRecipeints { get; set; }


    }
}