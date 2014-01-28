using System;
using FluentNHibernate.Data;

namespace Gecko.Bcon.Domain {
    public class EmergencyProfile : Entity {
        public virtual User User { get; set; }
        public virtual DateTime? LastUpdated { get; set; }
        public virtual string NextOfKinName { get; set; }
        public virtual string NextOfKinNumber { get; set; }
        public virtual string EmergencyNumber { get; set; }
        public virtual string EmergencyContact { get; set; }
        public virtual string NHSNumber{ get; set; }
        public virtual string KeyCode { get; set; }
        public virtual string Allergies { get; set; }
        public virtual string Medication { get; set; }
        public virtual string Conditions { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime? DOB { get; set; }
        public virtual DateTime? DateCreated { get; set; }
    }
}