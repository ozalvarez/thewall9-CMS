using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace thewall9.admin
{
    public static class APP
    {
        public static string _API_URL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["API_URL"];
            }
        }
    }
}