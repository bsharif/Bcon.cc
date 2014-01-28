using NHibernate;

namespace Gecko.Bcon.Web.Classes.Managers {
    /// <summary>
    /// BAse class for managers
    /// </summary>
    public abstract class ManagerBase : IManage
    {
        /// <summary>
        /// The default result
        /// </summary>
        public ManagerResult Result { get; set; }

        /// <summary>
        /// Database session
        /// </summary>
        public ISession DBSession { get; private set; }

        /// <summary>
        /// nHibernate database session
        /// </summary>
        /// <param name="session">nHibernate session</param>
        public ManagerBase(ISession session)
        {
            DBSession = session;
            Result = new ManagerResult();
        }

    }
}