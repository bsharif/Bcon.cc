using Gecko.Bcon.Domain;
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;

namespace Gecko.Bcon.Web.Areas.Admin.Models {
    public class UserGridViewModel {
        public IPagination<User> Results { get; set; }
        public GridSortOptions SortOptions { get; set; }
    }
}