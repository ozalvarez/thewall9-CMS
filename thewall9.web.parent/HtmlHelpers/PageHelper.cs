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
            if (CurrentPage < TotalPage)
            {
                TagBuilder _ul = new TagBuilder("ul");
                _ul.AddCssClass("pager");
                for (int i = 1; i <= TotalPage; i++)
                {
                    TagBuilder _li = new TagBuilder("li");
                    TagBuilder _a = new TagBuilder("a");
                    if (TotalPage != 1)
                    {
                        _a.MergeAttribute("href", PageUrl(i));
                        _a.InnerHtml = i.ToString();
                        if (i == CurrentPage)
                            _a.AddCssClass("disabled");

                    }
                    _li.InnerHtml += _a;
                    _ul.InnerHtml += _li;
                }
                return MvcHtmlString.Create(_ul.ToString());
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}