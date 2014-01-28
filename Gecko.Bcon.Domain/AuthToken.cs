using FluentNHibernate.Data;

namespace Gecko.Bcon.Domain {
    public class AuthToken : Entity {
        public virtual string Hash { get; set; }
    }
}