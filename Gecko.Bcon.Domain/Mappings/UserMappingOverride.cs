using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Gecko.Bcon.Domain.Mappings
{
    /// <summary>
    /// UserMappingOverride mapping override
    /// </summary>
    public class UserMappingOverride : IAutoMappingOverride<User> {
        public void Override(AutoMapping<User> mapping) {
            mapping.Map(x => x.Email).Length(100); //default value to 
            mapping.Map(x => x.Email).Unique(); //No duplicates please
        }
    }

    /// <summary>
    /// UserMappingOverride mapping override
    /// </summary>
    public class EmegencyProfileMappingOverride : IAutoMappingOverride<EmergencyProfile> {
        public void Override(AutoMapping<EmergencyProfile> mapping) {
            mapping.Map(x => x.Allergies).Length(Common.Constants.MAX_DBTEXT_STRING_SIZE);
            mapping.Map(x => x.Medication).Length(Common.Constants.MAX_DBTEXT_STRING_SIZE);
            mapping.Map(x => x.Conditions).Length(Common.Constants.MAX_DBTEXT_STRING_SIZE);
        }
    }
}