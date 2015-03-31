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
            catch (NullReferenceException e)
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
        public static List<CategoryWeb> GetCategories(this HtmlHelper helper)
        {
            return APP._Site.Categories;
        }
        public static List<ProductWeb> GetProducts(this HtmlHelper helper)
        {
            return APP._Site.Products;
        }
        private static string GetFriendlyUrlByAlias(this HtmlHelper helper, string Alias)
        {
            return APP._Site.Menu.Where(m => m.PageAlias.Equals(Alias)).Select(m => m.FriendlyUrl).SingleOrDefault();
        }
        public static MvcHtmlString GetFriendlyUrlByAliasNoHash(this HtmlHelper helper, string Alias)
        {
            return new MvcHtmlString(GetFriendlyUrlByAlias(helper, Alias).Replace("#", ""));
        }
        #region LANG
        public static List<CultureRoutes> GetLangs(this HtmlHelper helper)
        {
            return APP._Langs;
        }
        public static MvcHtmlString LinkUrl(this HtmlHelper helper, CultureRoutes Culture)
        {
            var _Request = helper.ViewContext.RequestContext.HttpContext.Request;
            return new MvcHtmlString("/change-lang?Lang=" + Culture.Name+"&FriendlyUrl="+_Request.Url.AbsolutePath);
        }
        public static MvcHtmlString LinkHome(this HtmlHelper helper)
        {
            return new MvcHtmlString("/" +APP._Langs.Where(m=>m.Name==APP._CurrentLang).FirstOrDefault().FriendlyUrl);
        }

        /*
         * SOCIAL METHODS
         */
        public static MvcHtmlString LinkTwitter(this HtmlHelper helper)
        {
            return new MvcHtmlString("http://twitter.com/"+APP._Langs.Where(m => m.Name == APP._CurrentLang).FirstOrDefault().Twitter);
        }
        public static MvcHtmlString LinkFacebook(this HtmlHelper helper)
        {
            return new MvcHtmlString("http://facebook.com/" + APP._Langs.Where(m => m.Name == APP._CurrentLang).FirstOrDefault().Facebook);
        }
        public static MvcHtmlString LinkTumblr(this HtmlHelper helper)
        {
            return new MvcHtmlString("http://" + APP._Langs.Where(m => m.Name == APP._CurrentLang).FirstOrDefault().Tumblr);
        }
        public static MvcHtmlString LinkGPlus(this HtmlHelper helper)
        {
            return new MvcHtmlString("http://plus.google.com/+" + APP._Langs.Where(m => m.Name == APP._CurrentLang).FirstOrDefault().GPlus);
        }
        public static MvcHtmlString LinkInstagram(this HtmlHelper helper)
        {
            return new MvcHtmlString("http://instagram.com/" + APP._Langs.Where(m => m.Name == APP._CurrentLang).FirstOrDefault().Instagram);
        }
        #endregion
    }
}
