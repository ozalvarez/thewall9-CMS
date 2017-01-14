using System.Web.Http.Filters;
namespace thewall9.api.ActionFilter
{
    public class ElmahHandledErrorLoggerFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            base.OnException(context);
        }
    }
}