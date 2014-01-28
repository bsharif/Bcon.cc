using NHibernate;

namespace Gecko.Bcon.Web.Classes.Managers {

    public interface IManage
    {
        ISession DBSession { get; }
        ManagerResult Result { get; set; }
    }
}