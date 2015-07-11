using Elmah;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;

namespace thewall9.web.parent.BLL
{
    public class ContentBLL : BaseBLL
    {
        public ContentBindingList Get(string Url, string Lang, string ContentAlias)
        {
            return DownloadObject<ContentBindingList>("api/content?SiteID=" + APP._SiteID
                + "&Url=" + Url
                + "&Lang=" + Lang
                + "&AliasList=" + ContentAlias);
        }
        public ContentBindingList Get(string Url, string ContentAlias)
        {
            return Get(Url, APP._CurrentLang, ContentAlias);
        }
    }
}
