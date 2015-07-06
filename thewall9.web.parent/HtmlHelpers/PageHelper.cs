using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace thewall9.web.parent.HtmlHelpers
{
    public static class PageHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html
            , int CurrentPage
            , int TotalPage
            , Func<int, String> PageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= TotalPage; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                if (TotalPage != 1)
                {
                    tag.MergeAttribute("href", PageUrl(i));
                    tag.InnerHtml = i.ToString();
                    if (i == CurrentPage)
                        tag.AddCssClass("selected");
                    result.AppendLine(tag.ToString());
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}