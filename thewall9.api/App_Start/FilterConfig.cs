using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using thewall9.api.ActionFilter;

namespace thewall9.api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
        }
        public static void RegisterHttpFilters(HttpFilterCollection filters)
        {
            filters.Add(new RuleExceptionHandlingAttribute());
            filters.Add(new ElmahHandledErrorLoggerFilter());
        }
    }
}
