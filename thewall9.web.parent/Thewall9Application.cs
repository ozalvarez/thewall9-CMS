using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace thewall9.web.parent
{
    public class Thewall9Application : System.Web.HttpApplication
    {
        protected void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError(e);
        }

        protected void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError(e);
        }

        //Dimiss 404 errors for ELMAH
        protected void FilterError(ExceptionFilterEventArgs e)
        {
            if (e.Exception.GetBaseException() is HttpException)
            {
                HttpException ex = (HttpException)e.Exception.GetBaseException();
                if (ex.Message.Contains("A potentially dangerous Request.Path value was detected from the client"))
                    e.Dismiss();
                else
                {
                    switch (ex.GetHttpCode())
                    {
                        case 404:
                            e.Dismiss();
                            break;
                        case 410:
                            e.Dismiss();
                            break;
                        case 406:
                            e.Dismiss();
                            break;
                    }
                }
            }
        }
    }
}
