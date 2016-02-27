using System.Linq;
using System.Web.Mvc;
using thewall9.web.parent;
namespace thewall9.web.parent.HtmlHelpers
{
    public static class GAMethods
    {
        /// <summary>
        /// Returns an error alert that lists each model error, much like the standard ValidationSummary only with
        /// altered markup for the Twitter bootstrap styles.
        /// </summary>
        public static MvcHtmlString GAScript(this HtmlHelper helper)
        {
            var _Script = @" <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create','" + APP._Site.Site.GAID + @"', 'auto');
        ga('send', 'pageview');

    </script>";
            return new MvcHtmlString(_Script.ToString());
        }
        public static MvcHtmlString GAScript_QAQ(this HtmlHelper helper)
        {
            var _Script = @" <script>
var _gaq = _gaq || [];
    _gaq.push(['_setAccount', '" + APP._Site.Site.GAID + @"']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        //ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();
    </script>";
            return new MvcHtmlString(_Script.ToString());
        }
        //DELETE ME
        public static MvcHtmlString Lang(this HtmlHelper helper)
        {
            return new MvcHtmlString(string.IsNullOrEmpty(thewall9.web.parent.APP._Site.Site.DefaultLang) ? "es" : APP._Site.Site.DefaultLang);
        }
        public static MvcHtmlString MetatagDescription(this HtmlHelper helper)
        {
            if (string.IsNullOrEmpty(helper.ViewBag.MetaDescription))
                return null;
            return new MvcHtmlString(string.Format("<meta name='description' content='{0}' />", helper.Raw(helper.ViewBag.MetaDescription)));
        }
        public static MvcHtmlString MetatagDescriptionContent(this HtmlHelper helper)
        {
            if (string.IsNullOrEmpty(helper.ViewBag.MetaDescription))
                return null;
            return new MvcHtmlString(helper.ViewBag.MetaDescription);
        }
        public static MvcHtmlString TitlePageText(this HtmlHelper helper)
        {
            if (string.IsNullOrEmpty(helper.ViewBag.Title))
                return null;
            return new MvcHtmlString(helper.ViewBag.Title);
        }
        public static MvcHtmlString GetSourceMedium(this HtmlHelper helper)
        {
            if (thewall9.web.parent.APP._Referer != null)
            {
                return new MvcHtmlString(thewall9.web.parent.APP._Referer);
            }
            return new MvcHtmlString(string.Empty);
        }

        /*OPEN GRAPH*/
        public static bool HasOpenGraph(this HtmlHelper helper)
        {
            if (helper.ViewBag.OpenGraph == null) return false;
            return true;
        }
        public static MvcHtmlString OpenGraphTitle(this HtmlHelper helper)
        {
            if (string.IsNullOrEmpty(helper.ViewBag.OpenGraph.Title))
                return null;
            return new MvcHtmlString(helper.ViewBag.OpenGraph.Title);
        }
        public static MvcHtmlString OpenGraphDescription(this HtmlHelper helper)
        {
            if (string.IsNullOrEmpty(helper.ViewBag.OpenGraph.Description))
                return null;
            return new MvcHtmlString(helper.ViewBag.OpenGraph.Description);
        }
        public static MvcHtmlString OpenGraphImage(this HtmlHelper helper)
        {
            if (string.IsNullOrEmpty(helper.ViewBag.OpenGraph.FileRead.MediaUrl))
                return null;
            return new MvcHtmlString(helper.ViewBag.OpenGraph.FileRead.MediaUrl);
        }
        public static MvcHtmlString OpenGraphTags(this HtmlHelper helper)
        {
            if (helper.ViewBag.OGraph != null)
            {
                var _HTML = @"
<meta property='og:title' content='" + helper.ViewBag.OGraph.OGraphTitle + @"' />
<meta property = 'og:url' content = '" + helper.ViewContext.HttpContext.Request.Url + @"' />
<meta property = 'og:description' content = '" + helper.ViewBag.OGraph.OGraphDescription + @"' />
<meta name = 'twitter:card' content = 'summary_large_image' >
<meta name = 'twitter:site' content = '" + helper.TwitterUsername() + @"' >
<meta name = 'twitter:creator' content = '" + helper.TwitterUsername() + @"' >
<meta name = 'twitter:title' content = '" + helper.ViewBag.OGraph.OGraphTitle + @"' >
<meta name = 'twitter:description' content = '" + helper.ViewBag.OGraph.OGraphDescription + @"' >";
                if (helper.ViewBag.OGraph.FileRead != null && helper.ViewBag.OGraph.FileRead.MediaUrl!=null)
                {
                    _HTML += @"
<meta property = 'og:image' content = '" + helper.ViewBag.OGraph.FileRead.MediaUrl + @"' />
<meta name = 'twitter:image' content = '" + helper.ViewBag.OGraph.FileRead.MediaUrl + @"' >";
                }
                return new MvcHtmlString(_HTML);
            }
            return new MvcHtmlString(string.Empty);
            //< meta property = 'og:site_name' content = 'Work&Go' />
            // < meta property = 'og:image' content = '@Html.FindValue(_LayoutContent, 'share - img')' />
            //  < meta name = 'twitter:image' content = '@Html.FindValue(_LayoutContent, 'share - img')' > ";
        }
    }
}