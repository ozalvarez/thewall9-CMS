using System.Web;
using System.Web.Mvc;
using thewall9.web.parent.ActionFilter;

namespace thewall9.web.parent
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Redirect301ActionFilter());
            filters.Add(new FilterBase(), 2);
        }
    }
}
