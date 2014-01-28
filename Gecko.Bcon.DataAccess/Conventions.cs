using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Gecko.Bcon.DataAccess
{
    /// <summary>
    /// Custom Conventions for fluent nHibernate , should be very few here customizations here 
    /// Convention over configuration
    /// </summary>
    public class Conventions : IUserTypeConvention, IHasManyConvention, IIdConvention, IReferenceConvention
    {
        public void Apply(IPropertyInstance target)
        {
            target.CustomType(target.Property.PropertyType);
        }

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType.IsEnum);
        }

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IOneToManyCollectionInstance instance)
        {
        }

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IIdentityInstance instance)
        {
        }

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IManyToOneInstance instance)
        {
        }
    }
}