using Gecko.Bcon.Domain;

namespace Gecko.Bcon.Web.Models {
    public abstract class BaseViewModel { }
    public class MemberViewModel :BaseViewModel {
        public bool HasProfile { get; set; }
        public MemberViewModel(EmergencyProfile profile) {
            if(profile != null) {
                HasProfile = true;
                KeyCode = profile.KeyCode;
            }
        }

        public string KeyCode { get; set; }
    }
}