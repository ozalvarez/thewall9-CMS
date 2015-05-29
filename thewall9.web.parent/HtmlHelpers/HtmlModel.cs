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
        public static ContentBindingList Find(this HtmlHelper helper, ContentBindingList Model, string Value)
        {
            return Model.Items.Where(m => m.ContentPropertyAlias.Equals(Value)).SingleOrDefault();
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

            return new MvcHtmlString("/"+APP._Langs.Where(m=>m.Name==APP._CurrentLang).FirstOrDefault().FriendlyUrl);
        }
        public static MvcHtmlString LinkCart(this HtmlHelper helper)
        {
            return new MvcHtmlString(APP._Site.EcommercePages.Where(m=>m.PageAlias=="cart").FirstOrDefault().FriendlyUrl);
        }
        public static MvcHtmlString LinkCheckout(this HtmlHelper helper)
        {
            return new MvcHtmlString(APP._Site.EcommercePages.Where(m => m.PageAlias == "checkout").FirstOrDefault().FriendlyUrl);
        }
        public static MvcHtmlString LinkCatalog(this HtmlHelper helper)
        {
            return new MvcHtmlString(APP._Site.EcommercePages.Where(m => m.PageAlias == "catalog").FirstOrDefault().FriendlyUrl);
        }
        public static MvcHtmlString LinkOrderSent(this HtmlHelper helper)
        {
            return new MvcHtmlString(APP._Site.EcommercePages.Where(m => m.PageAlias == "order-sent").FirstOrDefault().FriendlyUrl);
        }
        public static int GetCurrentCurrencyID(this HtmlHelper helper)
        {
            return APP._CurrentCurrencyID;
        }

        /*
         * SOCIAL METHODS
         */
        public static MvcHtmlString LinkTwitter(this HtmlHelper helper)
        {
            return new MvcHtmlString("http://twitter.com/" + APP._Langs.Where(m => m.Name == APP._CurrentLang).FirstOrDefault().Twitter);
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

        //MESSAGES
        public static MvcHtmlString MessagesJson(this HtmlHelper helper)
        {
            var _List = new List<MessageModel>();
            var _CL = FindItems(helper, APP._Site.ContentLayout, "messages");
            foreach (var item in _CL)
            {
                _List.Add(new MessageModel
                {
                    Alias = item.ContentPropertyAlias,
                    Value = item.ContentCultures.ToList()[0].ContentPropertyValue
                });
            }
            return new MvcHtmlString(Newtonsoft.Json.JsonConvert.SerializeObject(_List));
        }
    }
    public class MessageModel
    {
        public string Alias { get; set; }
        public string Value { get; set; }
    }
}
