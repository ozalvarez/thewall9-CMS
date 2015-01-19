using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using thewall9.bll.Exceptions;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.bll
{
    public class BaseBLL
    {
        public WebClient MyWebClient
        {
            get
            {
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                return client;
            }
        }
        public static string StorageUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["StorageUrl"];
            }
        }
        protected ApplicationDbContext db
        {
            get
            {
                var db = new ApplicationDbContext();
                return db;
            }
        }
        public static bool Can(int SiteID, string UserID, ApplicationDbContext _c)
        {
            var _Can = _c.SiteUserRoles.Where(m => m.UserID.Equals(UserID) && m.SiteID == SiteID && m.SiteUserType == SiteUserType.CONTENT).Any();
            if (_Can)
                return _Can;
            else throw new RuleException("Ese usuario no tiene permiso para manejar el menu");
        }
    }
}
