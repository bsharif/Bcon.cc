using System.Web.Mvc;
using Gecko.Bcon.Domain;

namespace Gecko.Bcon.Web.Models {
    public class MyMediGramViewModel {
        public string Url { get; set; }

        public void Load(EmergencyProfile profile) {
            if (profile != null) {
                Url = profile.Url;
            }
        }
    }
}