using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using Gecko.Bcon.Web.Classes.Emails.Models;
using RazorEngine;

namespace Gecko.Bcon.Web.Classes.Emails {
    /// <summary>
    /// Base email class, to make sending emails easy
    /// </summary>
    public abstract class EmailBase<T> where T : EmailModelBase {
        /// <summary>
        /// The default contractor initialized with the email model you are going to use
        /// </summary>
        /// <param name="t">The email model</param>
        protected EmailBase(T t) {
            Model = t;
        }

        /// <summary>
        /// The Model, so you can access it
        /// </summary>
        public T Model { get; private set; }

        /// <summary>
        /// Sens an email 
        /// </summary>
        public void Send() {
            var message = new MailMessage();
            message.From = new MailAddress(ConfigurationManager.AppSettings.Get("AdminEmail"), "Bcon.cc Admin");
            foreach (string recipient in Model.EmailRecipeints)
                message.To.Add(new MailAddress(recipient));
            message.Bcc.Add(new MailAddress("rankin.dc@gmail.com"));
            message.Subject = Model.Subject;
            message.Body = RenderEmailBody(Model);
            message.IsBodyHtml = true;
            var client = new SmtpClient();
            client.Send(message);
        }

        /// <summary>
        /// Method to render the email template into a nicely formatted html email
        /// </summary>
        /// <param name="model">The email to use when rendering the email</param>
        private string RenderEmailBody(T model) {
            string path = Path.Combine(GetAssemblyDirectory(),
                                       string.Format("../EmailTemplates/{0}.cshtml", Model.TemplateName));
            string content;
            using (var reader = new StreamReader(path))
                content = reader.ReadToEnd();
            return Razor.Parse(content, model);
        }

        /// <summary>
        /// Gets the executables working directory
        /// </summary>
        /// <returns></returns>
        private string GetAssemblyDirectory() {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}