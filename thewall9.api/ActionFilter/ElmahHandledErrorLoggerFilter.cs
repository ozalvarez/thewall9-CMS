using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using thewall9.bll.Exceptions;

namespace thewall9.api.ActionFilter
{
    public class ElmahHandledErrorLoggerFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            base.OnException(context);
            if (!(context.Exception is RuleException))
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
        }
    }
}