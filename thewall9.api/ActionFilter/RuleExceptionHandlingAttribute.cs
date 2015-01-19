
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using thewall9.bll.Exceptions;

namespace thewall9.api.ActionFilter
{
    public class RuleExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is RuleException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(((RuleException)context.Exception).RuleExceptionMessage),
                    ReasonPhrase = "RuleException"
                };
            }
            else base.OnException(context);
        }
    }
}
