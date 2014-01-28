using Gecko.Bcon.Domain;

namespace Gecko.Bcon.Web.Classes.Commands {
    public class UserRegistrationResult : BaseCommand {
        public User User { get; set; }
    }
}