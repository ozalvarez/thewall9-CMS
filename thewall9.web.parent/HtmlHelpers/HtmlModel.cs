using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using thewall9.data.binding;

namespace thewall9.web.parent.HtmlHelpers
{
    public static class HtmlModel
    {
        public static bool ExistValue(this HtmlHelper helper, ContentBindingList Model, string Value)
        {
            var _Item = Model.Items.Where(m => m.ContentPropertyAlias.Equals(Value)).SingleOrDefault();
            if (_Item != null && _Item.ContentCultures.ToList()[0].ContentPropertyValue != null)
            {
                return !string.IsNullOrEmpty(_Item.ContentCultures.ToList()[0].ContentPropertyValue);
            }
            return false;
        }
        public static MvcHtmlString FindValue(this HtmlHelper helper, PageWeb Model, string Value)
        {
            return FindValue(helper, Model.Content, Value);
        }
        public static MvcHtmlString FindValue(this HtmlHelper helper, ContentBindingList Model, string Value)
        {
            try
            {
                var _Item = Model.Items.Where(m => m.ContentPropertyAlias.Equals(Value)).SingleOrDefault();
                return new MvcHtmlString(_Item.ContentCultures.ToList()[0].ContentPropertyValue);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("Value=" + Value + " in " + Model.ContentPropertyAlias + " is NULL", e.InnerException);
            }
        }
        public static MvcHtmlString GetValue(this HtmlHelper helper, ContentBindingList Model)
        {
            return new MvcHtmlString(Model.ContentCultures.ToList()[0].ContentPropertyValue);

        }
        public static ContentBindingList Find(this HtmlHelper helper, PageWeb Model, string Value)
        {
            return Model.Content.Items.Where(m => m.ContentPropertyAlias.Equals(Value)).SingleOrDefault();
        }
        public static ICollection<ContentBindingList> FindItems(this HtmlHelper helper, ContentBindingList Model, string Value)
        {
            return Model.Items.Where(m => m.ContentPropertyAlias.Equals(Value)).SingleOrDefault().Items;
        }
        public static ICollection<ContentBindingList> FindItems(this HtmlHelper helper, PageWeb Model, string Value)
        {
            return FindItems(helper, Model.Content, Value);
        }
        public static List<PageCultureBinding> GetMenu(this HtmlHelper helper)
        {
            return APP._Site.Menu;
        }
        public static SiteFullBinding GetSite(this HtmlHelper helper)
        {
            return APP._Site;
        }
        private static string GetFriendlyUrlByAlias(this HtmlHelper helper, string Alias)
        {
            return APP._Site.Menu.Where(m => m.PageAlias.Equals(Alias)).Select(m => m.FriendlyUrl).SingleOrDefault();
        }
        public static MvcHtmlString GetFriendlyUrlByAliasNoHash(this HtmlHelper helper, string Alias)
        {
            return new MvcHtmlString(GetFriendlyUrlByAlias(helper, Alias).Replace("#", ""));
        }
    }
}
