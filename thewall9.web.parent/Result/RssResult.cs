using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using thewall9.data.binding;

namespace thewall9.web.parent.Result
{

    public class RssResult : ActionResult
    {
        private List<BlogPostWeb> _items;
        private string _title;
        private string _description;

        /// <summary>
        /// Initialises the RssResult
        /// </summary>
        /// <param name="items">The items to be added to the rss feed.</param>
        /// <param name="title">The title of the rss feed.</param>
        /// <param name="description">A short description about the rss feed.</param>
        public RssResult(IEnumerable<BlogPostWeb> items, string title, string description)
        {
            _items = new List<BlogPostWeb>(items);
            _title = title;
            _description = description;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineHandling = NewLineHandling.Entitize;

            context.HttpContext.Response.ContentType = "text/xml";
            using (XmlWriter _writer = XmlWriter.Create(context.HttpContext.Response.OutputStream, settings))
            {

                // Begin structure
                _writer.WriteStartElement("rss");
                _writer.WriteAttributeString("version", "2.0");
                _writer.WriteStartElement("channel");

                _writer.WriteElementString("title", _title);
                _writer.WriteElementString("description", _description);
                _writer.WriteElementString("link", context.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority));

                // Individual items
                _items.ForEach(x =>
                {
                    _writer.WriteStartElement("item");
                    _writer.WriteElementString("title", x.Title);
                    _writer.WriteElementString("description", x.Content);
                    _writer.WriteElementString("link", context.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + "/post/" + x.BlogPostID + "/" + x.FriendlyUrl);
                    if (!string.IsNullOrEmpty(x.FeatureImageUrl))
                    {
                        _writer.WriteStartElement("image");
                        _writer.WriteElementString("url", x.FeatureImageUrl);
                        _writer.WriteElementString("title", x.Title);
                        _writer.WriteElementString("link", context.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + "/post/" + x.BlogPostID + "/" + x.FriendlyUrl);
                        _writer.WriteEndElement();
                    }
                    _writer.WriteEndElement();
                });

                // End structure
                _writer.WriteEndElement();
                _writer.WriteEndElement();
            }
        }

    }
}
