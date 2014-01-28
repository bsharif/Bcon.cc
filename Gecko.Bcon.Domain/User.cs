using System;
using FluentNHibernate.Data;

namespace Gecko.Bcon.Domain {
    public class User : Entity {
        public virtual string ActivationCode { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual Roles Role { get; set; }
        public virtual States State { get; set; }
        public virtual string ProfileImage { get; set; }

        public enum States {
            PendingActivation,
            Active,
            Deactivated,
            PendingDeletion,
            Disabled
        }

        public enum Roles {
            Anonymous,
            Registered,
            HealthCareProfessional,
            VerfiedDoctor,
            Admin
        }
    }
}